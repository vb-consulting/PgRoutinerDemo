# Dictionary for database `companies_web_demo`

- Server: PostgreSQL `localhost:5434`, version `12.0`
- Local time stamp: `2021-04-19T19:00:42.4508425+02:00`
- Schema: public

## Table of Contents

- Table [`public.companies`](#table-publiccompanies)
- Table [`public.company_areas`](#table-publiccompany_areas)
- Function [`public.search_companies(json, integer, integer)`](#function-publicsearch_companiesjson-integer-integer)
## Tables

### Table `public.companies`

<!-- comment on table "public"."companies" is @until-end-tag; -->
Main table for compnaies data.
<!-- end -->

| Column |             | Type | Nullable | Default | Comment |
| ------ | ----------- | -----| -------- | ------- | ------- |
| <a id="user-content-public-companies-id" href="#public-companies-id">#</a>`id` | **PK** | `bigint` | **NO** | *auto increment* | <!-- comment on column "public"."companies"."id" is @until-end-tag; --><!-- end --> |
| <a id="user-content-public-companies-name" href="#public-companies-name">#</a>`name` |  | `character varying(256)` | **NO** |  | <!-- comment on column "public"."companies"."name" is @until-end-tag; --><!-- end --> |
| <a id="user-content-public-companies-name_normalized" href="#public-companies-name_normalized">#</a>`name_normalized` | **IDX** | `character varying(256)` | **NO** |  | <!-- comment on column "public"."companies"."name_normalized" is @until-end-tag; -->Company name in lowercase uniquely indexed. Search is on this field.<!-- end --> |
| <a id="user-content-public-companies-website" href="#public-companies-website">#</a>`website` |  | `character varying(1024)` | **NO** |  | <!-- comment on column "public"."companies"."website" is @until-end-tag; --><!-- end --> |
| <a id="user-content-public-companies-area_id" href="#public-companies-area_id">#</a>`area_id` | **FK [➝](#public-company_areas-id) `company_areas.id`** | `integer` | **NO** |  | <!-- comment on column "public"."companies"."area_id" is @until-end-tag; --><!-- end --> |
| <a id="user-content-public-companies-about" href="#public-companies-about">#</a>`about` |  | `character varying` | YES |  | <!-- comment on column "public"."companies"."about" is @until-end-tag; --><!-- end --> |
| <a id="user-content-public-companies-modified" href="#public-companies-modified">#</a>`modified` |  | `timestamp without time zone` | **NO** | `timezone('utc'::text, now())` | <!-- comment on column "public"."companies"."modified" is @until-end-tag; --><!-- end --> |

<a href="#table-of-contents" title="Table of Contents">&#8673;</a>

### Table `public.company_areas`

<!-- comment on table "public"."company_areas" is @until-end-tag; -->
List of all possible company areas.
<!-- end -->

| Column |             | Type | Nullable | Default | Comment |
| ------ | ----------- | -----| -------- | ------- | ------- |
| <a id="user-content-public-company_areas-id" href="#public-company_areas-id">#</a>`id` | **PK** | `integer` | **NO** | *auto increment* | <!-- comment on column "public"."company_areas"."id" is @until-end-tag; --><!-- end --> |
| <a id="user-content-public-company_areas-name" href="#public-company_areas-name">#</a>`name` |  | `character varying(256)` | **NO** |  | <!-- comment on column "public"."company_areas"."name" is @until-end-tag; --><!-- end --> |

<a href="#table-of-contents" title="Table of Contents">&#8673;</a>

## Routines

### Function `public.search_companies(json, integer, integer)`

- Returns `json`

- Language is `plpgsql`

<!-- comment on function "public"."search_companies"(json, integer, integer) is @until-end-tag; -->


Search companies by filter and return data page with results.

Parameters:

- `_filter` is `json` with following schema `{"search", "areaId"}`

- `page` page indexed from 1

- `page_size`, default is 25



Returning json schema:

`{"count", "page": {"id", "name", "website", "area", "about", "modified"}}`


<!-- end -->

<a href="#table-of-contents" title="Table of Contents">&#8673;</a>
