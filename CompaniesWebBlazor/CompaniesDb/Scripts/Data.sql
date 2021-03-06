DO $CompaniesWebDb_data$
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
--
-- Data for Name: company_areas; Type: TABLE DATA; Schema: public; Owner: postgres
--
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (1, 'IT');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (2, 'Healthcare');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (3, 'Finance');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (4, 'Trade');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (9, 'Manufacturing');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (10, 'Transportation');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (5, 'AI');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (6, 'Services');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (7, 'Marketing');
INSERT INTO public.company_areas OVERRIDING SYSTEM VALUE VALUES (8, 'Production');
--
-- Name: company_areas_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--
PERFORM pg_catalog.setval('public.company_areas_id_seq', 10, true);
--
-- PostgreSQL database dump complete
--
END $CompaniesWebDb_data$
LANGUAGE plpgsql;
