CREATE TABLE public.company_areas (
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name character varying(256) NOT NULL
);

COMMENT ON TABLE public.company_areas IS 'List of all possible company areas.';
