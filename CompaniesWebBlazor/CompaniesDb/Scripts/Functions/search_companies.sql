CREATE FUNCTION public.search_companies(_filter json, _page integer, _page_size integer DEFAULT 25) RETURNS json
    LANGUAGE plpgsql SECURITY DEFINER
    AS $$
declare
    _search varchar;
    _area_id int;
begin
    _search := _filter->'search';
    _area_id := _filter->'area_id';
    
    if _search = '' then
        _search := null;
    end if;
    
    if _search is not null then
        _search := '%' || lower(_search) || '%';
    end if;
    create temp table companies_tmp on commit drop as
    select 
        id
    from 
        companies
    where
        (_search is null or name_normalized like _search)
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
                        c.id,
                        name,
                        website,
                        ca.name as area,
                        about,
                        modified
                    from 
                        companies_tmp t
                        inner join companies c on t.id = c.id
                        inner join company_areas ca on c.area_id = ca.id
                    order by
                            l.id
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

COMMENT ON FUNCTION public.search_companies(_filter json, _page integer, _page_size integer) IS '
Search companies by filter and return data page with results.
Parameters:
- `_filter` is `json` with following schema `{"search", "area_id"}`
- `page` page indexed from 1
- `page_size`, default is 25
Returning json schema:
`{"count", {"id", "name", "website", "area", "about", "modified"}}`
';
