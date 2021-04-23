# `PgRoutiner` Demo

This repository contains the demo application(s) for the `PgRoutiner` .NET tool for PostgreSQL databases

The project demonstrates how you can utilize the `PgRoutiner` .NET tool to rapidly build applications and prototypes based on the PostgreSQL database.

It also represents an **alternative to the classic ORM approach** of building database applications by combining advanced PostgreSQL concepts with the database-first approach and letting the `PgRoutiner` generate automatically all necessary tedious code, that otherwise, you would have to write yourself.

`PgRoutiner` is a .NET global tool that can be run manually from the command line.

It uses **.NET configuration files** or **command-line parameters** - and it can be set in a project pre-build stage so it can generate all necessary code before each build.

Historically, all disadvantages of database-first are addressed with the `PgRoutiner`, such as:

- Generates tedious C# source code, so you don't have to - for the objects that are already defined in a database such as:
  - Function or procedure calls
  - Create (insert), create returning, create returning, create on conflict do nothing, create on conflict do update, create on conflict do nothing returning, create on conflict do update returning
  - Read (select) by (keys), read all
  - Update, update returning
  - Delete, delete returning
  - All necessary models as DTO's or Records
  - Unit test project with unit test templates for each generated methods
- Ability to track all database artifacts and objects in source control systems such as git. `PgRoutiner` creates any script you need:
  - SQL Script for the entire schema
  - SQL Script for data in all or selected tables
  - SQL Script in a separate SQL file for each object spread across project directories for easier navigation (tables, views, functions, procedures, domains, types, schemas, and sequences)
  - Difference Script between two connection for automatically generated SQL migration scripts
- Ability to keep database comments in sync with your project documentation by generating markdown files and committing changes back to the database.
- Ability to utilize testing or TDD out-of-the-box for your PostgreSQL database project
- Increased ability to migrate to another database system by having covered all functionalities with unit tests. This feature is not yet on a level of classic ORM - but it will be once support for other database types is developed
- Object-relational impedance mismatch is non-existing. See the approach described in this [article](https://www.enterprisedb.com/blog/how-no-object-relational-mapping-norm-improves-application-performance-postgresql)

High level concept info-graphics:

<img src="https://raw.githubusercontent.com/vb-consulting/PgRoutinerDemo/master/pgroutiner%20-%20concept.png" alt="concept" width="640"/>

## Demo

Demo application is implemented as ***Blazor WebAssembly*** application. Here is the mockup:

<img src="https://raw.githubusercontent.com/vb-consulting/PgRoutinerDemo/master/mockup.svg" alt="drawing" width="640"/>

## Installation

1. Make sure you have installed dotnet 5 (try `$ dotnet --info`) and access to the PostgreSQL (preferred at least 12).

Note: `PgRoutiner` will require access to the `pg_dump` program that is run as an external process to generate SQL scripts.
It is advisable to have local PostgreSQL installation but not required. 
If there is local installation, `PgRoutiner` will find a path to the `pg_dump` automatically.
If there is no local installation, the path to the `pg_dump` must be set manually in configuration. 
See `PgRoutiner` wiki for more details.

2. Clone this repository.

3. Install `PgRoutiner` global tool:
  
```txt
$ dotnet tool install --global dotnet-pgroutiner
You can invoke the tool using the following command: PgRoutiner
Tool 'dotnet-pgroutiner' (version '3.3.5') was successfully installed.
```

4. Create an empty database on your PostgreSQL server. For example `CREATE DATABASE companies_web_demo;`

5. Adjust connection strings to point to your database with a valid user that has grants for reading and writing. Connection string is located in a [Blazor server application configuration](https://github.com/vb-consulting/PgRoutinerDemo/blob/master/CompaniesWebBlazor/Server/appsettings.Development.json):

6. Recreate entire schema in database by executing schema [script file](https://github.com/vb-consulting/PgRoutinerDemo/blob/master/CompaniesWebBlazor/CompaniesDb/Scripts/Schema.sql) and [data script file](https://github.com/vb-consulting/PgRoutinerDemo/blob/master/CompaniesWebBlazor/CompaniesDb/Scripts/Data.sql).

Note: these are automatically generated script, it will be recreated by PgRoutiner every time you run it, until you disabled it in a configuration [here](https://github.com/vb-consulting/PgRoutinerDemo/blob/master/CompaniesWebBlazor/CompaniesDb/appsettings.PgRoutiner.json#L119) and [here](https://github.com/vb-consulting/PgRoutinerDemo/blob/master/CompaniesWebBlazor/CompaniesDb/appsettings.PgRoutiner.json#L136)

You can, of course, use any available file to do so, or, you can simply use the `PgRoutiner` `-x` switch to execute them from the `CompanyDb` dir:

```
PgRoutinerDemo/CompaniesWebBlazor/CompaniesDb$ pgroutiner -x Scripts/Schema.sql
...
PgRoutinerDemo/CompaniesWebBlazor/CompaniesDb$ pgroutiner -x Scripts/Data.sql
...
```

Note: running `PgRoutiner` from certain dir, always looks up .NET configuration files for a connection.

6. Run "insert seed data" unit test to insert bogus data into the database:

```
 $ dotnet test CompaniesWebBlazor/CompaniesDbTests/CompaniesDbTests.csproj --filter "CompaniesDbTests._InsertSeedData"
```

You can verify that bogus data has been inserted by running the query with `PgRoutiner` :

```
PgRoutinerDemo/CompaniesWebBlazor/CompaniesDb$ pgroutiner -x "select * from companies"
...
```

7. Run .NET in a Blazor server project:

```
PgRoutinerDemo/CompaniesWebBlazor/Server$ dotnet run
```

## About the project

This project started as a collection of personal tools that would help me in the development of .NET applications backed by PostgreSQL and it has grown into something much bigger.

There is a list of planned features that are waiting to be implemented such for example:

- GUI with PostgreSQL code editor. There is a prototype of PostgreSQL editor already that can be combined with code generation settings.
- Website and tutorials
- More code generators, for endpoints, support for more languages, etc...

However, for now, this is just an open-source side-project, so without sponsorship, these features will have to wait until I have some more time. If you are interested in support and sponsorship, you can contact me via [linkedin](https://www.linkedin.com/in/vb-software/)

## Licence
 
Copyright (c) Vedran Bilopavlović - VB Consulting and VB Software 2021
This source code is licensed under the [MIT license](https://github.com/vbilopav/NoOrm.Net/blob/master/LICENSE).
