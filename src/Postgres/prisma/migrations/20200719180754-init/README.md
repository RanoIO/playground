# Migration `20200719180754-init`

This migration has been generated at 7/19/2020, 6:07:54 PM.
You can check out the [state of the schema](./schema.prisma) after the migration.

## Database Steps

```sql
CREATE TABLE "public"."User" (
"email" text  NOT NULL ,
"firstName" text  NOT NULL ,
"id" text  NOT NULL ,
"lastName" text  NOT NULL ,
"password" text  NOT NULL ,
"passwordHash" text  NOT NULL ,
"passwordSalt" text  NOT NULL ,
    PRIMARY KEY ("id"))

CREATE TABLE "public"."GoogleAccount" (
"email" text  NOT NULL ,
"id" text  NOT NULL ,
"profileId" text  NOT NULL ,
"userId" text  NOT NULL ,
    PRIMARY KEY ("id"))

CREATE TABLE "public"."Project" (
"id" text  NOT NULL ,
"name" text  NOT NULL ,
    PRIMARY KEY ("id"))

CREATE TABLE "public"."TestCase" (
"id" text  NOT NULL ,
"name" text  NOT NULL ,
    PRIMARY KEY ("id"))

CREATE UNIQUE INDEX "User.email" ON "public"."User"("email")

CREATE UNIQUE INDEX "GoogleAccount.email" ON "public"."GoogleAccount"("email")

CREATE UNIQUE INDEX "GoogleAccount_userId" ON "public"."GoogleAccount"("userId")

ALTER TABLE "public"."GoogleAccount" ADD FOREIGN KEY ("userId")REFERENCES "public"."User"("id") ON DELETE CASCADE  ON UPDATE CASCADE
```

## Changes

```diff
diff --git schema.prisma schema.prisma
migration ..20200719180754-init
--- datamodel.dml
+++ datamodel.dml
@@ -1,0 +1,49 @@
+// This is your Prisma schema file,
+// learn more about it in the docs: https://pris.ly/d/prisma-schema
+
+datasource db {
+  provider = "postgresql"
+  url = "***"
+}
+
+// generator client {
+//   provider = "prisma-client-js"
+// }
+
+model User {
+  id String @id @default(uuid())
+  firstName String
+  lastName String
+
+  email String @unique
+  password String
+  passwordSalt String
+  passwordHash String
+
+  googleAccount GoogleAccount?
+}
+
+
+model GoogleAccount {
+  id String @id @default(uuid())
+
+  profileId String
+  email String @unique
+
+  user User @relation(fields: [userId], references: [id])
+  userId String
+}
+
+
+model Project {
+  id String @id @default(uuid())
+
+  name String
+}
+
+
+model TestCase {
+  id String @id @default(uuid())
+
+  name String
+}
```


