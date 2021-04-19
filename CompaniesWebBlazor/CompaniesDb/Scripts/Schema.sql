DO $CompaniesDb_schema$
BEGIN
--
-- PostgreSQL database dump
--
-- Dumped from database version 12.0
-- Dumped by pg_dump version 12.0
SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
PERFORM pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;
ALTER TABLE IF EXISTS ONLY public.companies DROP CONSTRAINT IF EXISTS fk_area_id;
DROP INDEX IF EXISTS public.idx_companies_name_normalized;
ALTER TABLE IF EXISTS ONLY public.company_areas DROP CONSTRAINT IF EXISTS company_areas_pkey;
ALTER TABLE IF EXISTS ONLY public.companies DROP CONSTRAINT IF EXISTS companies_pkey;
DROP TABLE IF EXISTS public.company_areas;
DROP TABLE IF EXISTS public.companies;
DROP FUNCTION IF EXISTS public.search_companies(_filter json, _page integer, _page_size integer);
--
-- Name: search_companies(json, integer, integer); Type: FUNCTION; Schema: public; Owner: -
--
CREATE FUNCTION public.search_companies(_filter json, _page integer, _page_size integer DEFAULT 25) RETURNS json
    LANGUAGE plpgsql SECURITY DEFINER
    AS $$
declare
    _search varchar;
    _area_id int;
begin
    _search := _filter->>'search';
    _area_id := _filter->>'areaId';
    
    if _search = '' then
        _search := null;
    end if;
    
    if _search is not null then
        _search := lower(_search);
    end if;
    
    raise info '%', _search;
    raise info '%', _area_id;
    
    create temp table companies_tmp on commit drop as
    select 
        id
    from 
        companies
    where
        (_search is null or name_normalized = _search or name_normalized like '%' || _search || '%')
        and (_area_id is null or area_id = _area_id);
                
    return json_build_object(
        'count', (select count(*) from companies_tmp),
        'page', (
            coalesce(
                (select json_agg(
                    json_build_object(
                        'id', id,
                        'name', name,
                        'website', website,
                        'area', area,
                        'about', about,
                        'modified', modified
                    ) 
                )
                from ( 
                    
                    select 
                        t.id,
                        c.name,
                        website,
                        ca.name as area,
                        about,
                        modified
                    from 
                        companies_tmp t
                        inner join companies c on t.id = c.id
                        inner join company_areas ca on c.area_id = ca.id
                    order by
                            t.id
                    limit
                        _page_size
                    offset 
                        _page_size * (_page - 1)
                        
                ) sub
            ), 
            '[]'::json))
    );
end
$$;
--
-- Name: FUNCTION search_companies(_filter json, _page integer, _page_size integer); Type: COMMENT; Schema: public; Owner: -
--
COMMENT ON FUNCTION public.search_companies(_filter json, _page integer, _page_size integer) IS '
Search companies by filter and return data page with results.
Parameters:
- `_filter` is `json` with following schema `{"search", "areaId"}`
- `page` page indexed from 1
- `page_size`, default is 25
Returning json schema:
`{"count", "page": {"id", "name", "website", "area", "about", "modified"}}`
';
SET default_tablespace = '';
SET default_table_access_method = heap;
--
-- Name: companies; Type: TABLE; Schema: public; Owner: -
--
CREATE TABLE public.companies (
    id bigint NOT NULL,
    name character varying(256) NOT NULL,
    name_normalized character varying(256) NOT NULL,
    website character varying(1024) NOT NULL,
    area_id integer NOT NULL,
    about character varying,
    modified timestamp without time zone DEFAULT timezone('utc'::text, now()) NOT NULL
);
--
-- Name: TABLE companies; Type: COMMENT; Schema: public; Owner: -
--
COMMENT ON TABLE public.companies IS 'Main table for compnaies data.';
--
-- Name: COLUMN companies.name_normalized; Type: COMMENT; Schema: public; Owner: -
--
COMMENT ON COLUMN public.companies.name_normalized IS 'Company name in lowercase uniquely indexed. Search is on this field.';
--
-- Name: companies_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--
ALTER TABLE public.companies ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.companies_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
--
-- Name: company_areas; Type: TABLE; Schema: public; Owner: -
--
CREATE TABLE public.company_areas (
    id integer NOT NULL,
    name character varying(256) NOT NULL
);
--
-- Name: TABLE company_areas; Type: COMMENT; Schema: public; Owner: -
--
COMMENT ON TABLE public.company_areas IS 'List of all possible company areas.';
--
-- Name: company_areas_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--
ALTER TABLE public.company_areas ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.company_areas_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
--
-- Name: companies companies_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--
ALTER TABLE ONLY public.companies
    ADD CONSTRAINT companies_pkey PRIMARY KEY (id);
--
-- Name: company_areas company_areas_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--
ALTER TABLE ONLY public.company_areas
    ADD CONSTRAINT company_areas_pkey PRIMARY KEY (id);
--
-- Name: idx_companies_name_normalized; Type: INDEX; Schema: public; Owner: -
--
CREATE UNIQUE INDEX idx_companies_name_normalized ON public.companies USING btree (name_normalized);
--
-- Name: companies fk_area_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--
ALTER TABLE ONLY public.companies
    ADD CONSTRAINT fk_area_id FOREIGN KEY (area_id) REFERENCES public.company_areas(id) DEFERRABLE;
--
-- PostgreSQL database dump complete
--
END $CompaniesDb_schema$
LANGUAGE plpgsql;
