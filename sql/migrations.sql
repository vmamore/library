CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE SCHEMA IF NOT EXISTS inventory;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE SCHEMA IF NOT EXISTS rentals;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE TABLE inventory.books (
        "Id" uuid NOT NULL,
        "Title" character varying(150) NOT NULL,
        "Author" character varying(150) NOT NULL,
        "ReleasedYear" character varying(12) NOT NULL,
        "Pages" integer NOT NULL,
        "Version" integer NOT NULL,
        "ISBN" character varying(100) NULL,
        CONSTRAINT "PK_books" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE TABLE rentals.books (
        "Id" uuid NOT NULL,
        "Title" character varying(150) NOT NULL,
        "Author" character varying(150) NOT NULL,
        "Status" integer NOT NULL,
        CONSTRAINT "PK_books" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE TABLE rentals.librarians (
        "Id" uuid NOT NULL,
        "FirstName" text NULL,
        "LastName" text NULL,
        "BirthDate" timestamp without time zone NULL,
        "Street" character varying(100) NULL,
        "City" character varying(50) NULL,
        "Number" character varying(10) NULL,
        "District" character varying(20) NULL,
        "Cpf" text NULL,
        CONSTRAINT "PK_librarians" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE TABLE rentals.locators (
        "Id" uuid NOT NULL,
        "FirstName" text NULL,
        "LastName" text NULL,
        "BirthDate" timestamp without time zone NULL,
        "Street" character varying(100) NULL,
        "City" character varying(50) NULL,
        "Number" character varying(10) NULL,
        "District" character varying(20) NULL,
        "Cpf" text NULL,
        CONSTRAINT "PK_locators" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE TABLE rentals.penalties (
        "Id" uuid NOT NULL,
        "LocatorId" uuid NULL,
        "CreatedDate" timestamp without time zone NOT NULL,
        "EndDate" timestamp without time zone NOT NULL,
        "Reason" text NULL,
        CONSTRAINT "PK_penalties" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_penalties_locators_LocatorId" FOREIGN KEY ("LocatorId") REFERENCES rentals.locators ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE TABLE rentals.rentals (
        "Id" uuid NOT NULL,
        "RentedDay" timestamp without time zone NOT NULL,
        "DayToReturn" timestamp without time zone NULL,
        "ReturnedDay" timestamp without time zone NULL,
        "Status" integer NOT NULL,
        "LocatorId" uuid NULL,
        "LibrarianId" uuid NULL,
        CONSTRAINT "PK_rentals" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_rentals_librarians_LibrarianId" FOREIGN KEY ("LibrarianId") REFERENCES rentals.librarians ("Id"),
        CONSTRAINT "FK_rentals_locators_LocatorId" FOREIGN KEY ("LocatorId") REFERENCES rentals.locators ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE TABLE rentals.booksrentals (
        "BookId" uuid NOT NULL,
        "BookRentalId" uuid NOT NULL,
        CONSTRAINT "PK_booksrentals" PRIMARY KEY ("BookId", "BookRentalId"),
        CONSTRAINT "FK_booksrentals_books_BookId" FOREIGN KEY ("BookId") REFERENCES rentals.books ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_booksrentals_rentals_BookRentalId" FOREIGN KEY ("BookRentalId") REFERENCES rentals.rentals ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE INDEX "IX_booksrentals_BookRentalId" ON rentals.booksrentals ("BookRentalId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE INDEX "IX_penalties_LocatorId" ON rentals.penalties ("LocatorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE INDEX "IX_rentals_LibrarianId" ON rentals.rentals ("LibrarianId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    CREATE INDEX "IX_rentals_LocatorId" ON rentals.rentals ("LocatorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210905001829_InitialMigration') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210905001829_InitialMigration', '6.0.0-preview.6.21352.1');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210908002807_AddPhotoUrlColumnToBooksTable') THEN
    ALTER TABLE rentals.books ADD "PhotoUrl" character varying(450) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210908002807_AddPhotoUrlColumnToBooksTable') THEN
    ALTER TABLE inventory.books ADD "PhotoUrl" character varying(450) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20210908002807_AddPhotoUrlColumnToBooksTable') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20210908002807_AddPhotoUrlColumnToBooksTable', '6.0.0-preview.6.21352.1');
    END IF;
END $EF$;
COMMIT;

