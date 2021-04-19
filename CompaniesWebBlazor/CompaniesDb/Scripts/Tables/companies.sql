CREATE TABLE public.companies (
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name character varying(256) NOT NULL,
    name_normalized character varying(256) NOT NULL,
    website character varying(1024) NOT NULL,
    area_id integer NOT NULL,
    about character varying,
    modified timestamp without time zone DEFAULT timezone('utc'::text, now()) NOT NULL,
    CONSTRAINT fk_area_id FOREIGN KEY (area_id) REFERENCES public.company_areas(id) DEFERRABLE
);

COMMENT ON TABLE public.companies IS 'Main table for compnaies data.';
COMMENT ON COLUMN public.companies.name_normalized IS 'Company name in lowercase uniquely indexed. Search is on this field.';
CREATE UNIQUE INDEX idx_companies_name_normalized ON public.companies USING btree (name_normalized);
