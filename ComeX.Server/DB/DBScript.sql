CREATE TABLE "users" (
  "id" uuid PRIMARY KEY,
  "username" text
);

CREATE TABLE "user_tokens" (
  "tokenHash" text PRIMARY KEY,
  "userId" uuid,
  "validFrom" timestamp,
  "validTo" timestamp
);

CREATE TABLE "rooms" (
  "id" uuid PRIMARY KEY,
  "name" text,
  "isArchived" boolean
);

CREATE TABLE "user_to_room" (
  "userId" uuid,
  "roomId" uuid
);

CREATE TABLE "messages" (
  "id" uuid PRIMARY KEY,
  "authorId" uuid,
  "hasFile" boolean,
  "roomId" uuid,
  "sendTime" timestamp,
  "parentId" uuid,
  "content" text
);

CREATE TABLE "reactions" (
  "id" uuid PRIMARY KEY,
  "userId" uuid,
  "messageId" uuid,
  "emoji" text
);

CREATE TABLE "surveys" (
  "id" uuid PRIMARY KEY,
  "authorId" uuid,
  "roomId" uuid,
  "sendTime" timestamp,
  "question" text,
  "isMultipleChoice" boolean
);

CREATE TABLE "answers" (
  "id" uuid PRIMARY KEY,
  "content" text,
  "surveyId" uuid
);

CREATE TABLE "votes" (
  "id" uuid PRIMARY KEY,
  "userId" uuid,
  "answerId" uuid
);

CREATE TABLE "blocked_users" (
  "userId" uuid
);

ALTER TABLE "user_tokens" ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "user_to_room" ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "user_to_room" ADD FOREIGN KEY ("roomId") REFERENCES "rooms" ("id");

ALTER TABLE "messages" ADD FOREIGN KEY ("authorId") REFERENCES "users" ("id");

ALTER TABLE "messages" ADD FOREIGN KEY ("roomId") REFERENCES "rooms" ("id");

ALTER TABLE "messages" ADD FOREIGN KEY ("parentId") REFERENCES "messages" ("id");

ALTER TABLE "reactions" ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "reactions" ADD FOREIGN KEY ("messageId") REFERENCES "messages" ("id");

ALTER TABLE "surveys" ADD FOREIGN KEY ("authorId") REFERENCES "users" ("id");

ALTER TABLE "surveys" ADD FOREIGN KEY ("roomId") REFERENCES "rooms" ("id");

ALTER TABLE "answers" ADD FOREIGN KEY ("surveyId") REFERENCES "surveys" ("id");

ALTER TABLE "votes" ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");

ALTER TABLE "votes" ADD FOREIGN KEY ("answerId") REFERENCES "answers" ("id");

ALTER TABLE "blocked_users" ADD FOREIGN KEY ("userId") REFERENCES "users" ("id");
