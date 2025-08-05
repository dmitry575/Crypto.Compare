CREATE DATABASE "CryptoComapre";

CREATE TABLE IF NOT EXISTS public."SymbolProviders"
(
"Id" bigserial NOT NULL,
"SymbolName" varchar(64) NOT NULL,
"ProviderName" varchar(64) NOT NULL,
"PriceSell" decimal(32,18) NOT NULL,
"PriceBuy" decimal(32,18) NOT NULL,
"UpdatedAt" timestamp without time zone NOT NULL
);

CREATE UNIQUE INDEX IF NOT EXISTS symbolproviders_symbol ON "SymbolProviders" USING btree ("SymbolName","ProviderName");



