CREATE TABLE "Books" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Books" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "ISBN" TEXT NOT NULL,
    "Authors" TEXT NOT NULL,
    "NumberOfPages" INTEGER NOT NULL,
    "Publisher" TEXT NOT NULL,
    "MediaType" INTEGER NOT NULL,
    "Country" TEXT NOT NULL,
    "ReleaseDate" TEXT NOT NULL
);

CREATE TABLE "Characters" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Characters" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Culture" TEXT NULL,
    "Born" TEXT NULL,
    "Died" TEXT NULL,
    "Gender" INTEGER NOT NULL,
    "Aliases" TEXT NULL,
    "Titles" TEXT NULL,
    "TvSeries" TEXT NULL,
    "PlayedBy" TEXT NULL,
    "FatherId" INTEGER NULL REFERENCES "Characters" ("Id") DEFERRABLE INITIALLY DEFERRED,
    "MotherId" INTEGER NULL REFERENCES "Characters" ("Id") DEFERRABLE INITIALLY DEFERRED,
    "SpouseId" INTEGER NULL REFERENCES "Characters" ("Id") DEFERRABLE INITIALLY DEFERRED
);

CREATE TABLE "BookCharacters" (
    "BookId" INTEGER NOT NULL REFERENCES "Books" ("Id"),
    "CharacterId" INTEGER NOT NULL REFERENCES "Characters" ("Id"),
    "Type" INTEGER NOT NULL,

    PRIMARY KEY ("BookId", "CharacterId", "Type")

);

CREATE TABLE "Houses" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Houses" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "CoatOfArms" TEXT NULL,
    "Words" TEXT NULL,
    "Region" TEXT NULL,
    "Founded" TEXT NULL,
    "DiedOut" TEXT NULL,
    "Seats" TEXT NULL,
    "Titles" TEXT NULL,
    "AncestralWeapons" TEXT NULL,
    "FounderId" INTEGER NULL REFERENCES "Characters" ("Id"),
    "CurrentLordId" INTEGER NULL REFERENCES "Characters" ("Id"),
    "HeirId" INTEGER NULL REFERENCES "Characters" ("Id"),
    "MainBranchId" INTEGER NULL REFERENCES "Houses" ("Id") DEFERRABLE INITIALLY DEFERRED,
    "OverlordId" INTEGER NULL REFERENCES "Houses" ("Id") DEFERRABLE INITIALLY DEFERRED
);

CREATE TABLE "HouseCharacters" (
    "CharacterId" INTEGER NOT NULL REFERENCES "Characters" ("Id"),
    "HouseId" INTEGER NOT NULL REFERENCES "Houses" ("Id"),

    PRIMARY KEY ("CharacterId", "HouseId")
);


--CREATE INDEX "IX_BookCharacters_CharactersId" ON "BookCharacters" ("CharactersId");

--CREATE INDEX "IX_BookPovCharacters_PovCharactersId" ON "BookPovCharacters" ("PovCharactersId");

--CREATE UNIQUE INDEX "IX_Characters_FatherId" ON "Characters" ("FatherId");

--CREATE UNIQUE INDEX "IX_Characters_MotherId" ON "Characters" ("MotherId");

--CREATE UNIQUE INDEX "IX_Characters_SpouseId" ON "Characters" ("SpouseId");

--CREATE INDEX "IX_HouseCharacters_HousesId" ON "HouseCharacters" ("HousesId");

--CREATE UNIQUE INDEX "IX_Houses_CurrentLordId" ON "Houses" ("CurrentLordId");

--CREATE UNIQUE INDEX "IX_Houses_FounderId" ON "Houses" ("FounderId");

--CREATE UNIQUE INDEX "IX_Houses_HeirId" ON "Houses" ("HeirId");

--CREATE INDEX "IX_Houses_MainBranchId" ON "Houses" ("MainBranchId");

--CREATE INDEX "IX_Houses_OverlordId" ON "Houses" ("OverlordId");
