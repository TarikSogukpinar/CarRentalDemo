-- This script was generated by a beta version of the ERD tool in pgAdmin 4.
-- Please log an issue at https://redmine.postgresql.org/projects/pgadmin4/issues/new if you find any bugs, including reproduction steps.
BEGIN;


CREATE TABLE public.companies
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    company_name character varying NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE public.departments
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    department_name character varying NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE public.employees
(
    user_id integer NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    department_id integer NOT NULL,
    PRIMARY KEY (user_id)
);

CREATE TABLE public.employer_activation_employees
(
    id integer NOT NULL,
    employer_user_id integer NOT NULL,
    confirmed_employee_user_id integer NOT NULL,
    confirmed_date date NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE public.employers
(
    user_id integer NOT NULL,
    company_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    web_site character varying(50) NOT NULL,
    phone_number character varying(15) NOT NULL,
    is_activated bit(1) NOT NULL,
    company_email character varying(50) NOT NULL,
    password character varying(50) NOT NULL,
    confirmpassword character varying(50) NOT NULL,
    PRIMARY KEY (user_id)
);

CREATE TABLE public.employers_activation_codes
(
    id integer NOT NULL,
    employer_user_id integer NOT NULL,
    activation_code character varying(100) NOT NULL,
    is_confirmed bit(1) NOT NULL,
    confirmed_date date,
    PRIMARY KEY (id)
);

CREATE TABLE public.job_positions
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    job_title character varying NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE public.system_users
(
    "userıd" integer NOT NULL,
    roles character varying(255),
    PRIMARY KEY ("userıd")
);

CREATE TABLE public.unemployeds
(
    user_id integer NOT NULL,
    first_name character varying NOT NULL,
    last_name character varying NOT NULL,
    identity_number character varying NOT NULL,
    birth_of_date date NOT NULL,
    PRIMARY KEY (user_id)
);

CREATE TABLE public.users
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    email character varying(50) NOT NULL,
    password character varying(50) NOT NULL,
    confirmpassword character varying(50) NOT NULL,
    firstname character varying(50) NOT NULL,
    lastname character varying(50) NOT NULL,
    national_id character varying(255) NOT NULL,
    PRIMARY KEY (id)
);

ALTER TABLE public.employees
    ADD FOREIGN KEY (department_id)
    REFERENCES public.departments (id)
    NOT VALID;


ALTER TABLE public.employees
    ADD FOREIGN KEY (user_id)
    REFERENCES public.users (id)
    NOT VALID;


ALTER TABLE public.employer_activation_employees
    ADD FOREIGN KEY (confirmed_employee_user_id)
    REFERENCES public.employees (user_id)
    NOT VALID;


ALTER TABLE public.employer_activation_employees
    ADD FOREIGN KEY (employer_user_id)
    REFERENCES public.employers (user_id)
    NOT VALID;


ALTER TABLE public.employers
    ADD FOREIGN KEY (company_id)
    REFERENCES public.companies (id)
    NOT VALID;


ALTER TABLE public.employers
    ADD FOREIGN KEY (user_id)
    REFERENCES public.users (id)
    NOT VALID;


ALTER TABLE public.employers_activation_codes
    ADD FOREIGN KEY (employer_user_id)
    REFERENCES public.employers (user_id)
    NOT VALID;


ALTER TABLE public.unemployeds
    ADD FOREIGN KEY (user_id)
    REFERENCES public.users (id)
    NOT VALID;

END;
