-- CREATE TABLE accounts (
--     id VARCHAR(255),
--     name VARCHAR(255),
--     email VARCHAR(255),
--     picture VARCHAR(255),

--     PRIMARY KEY (id)
-- );

-- CREATE TABLE blogs (
--     id INT NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,
--     title VARCHAR(255) NOT NULL,
--     body VARCHAR(255) NOT NULL,
--     imgUrl VARCHAR(255) NOT NULL,
--     published TINYINT NOT NULL,

--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId)
--         REFERENCES accounts (id)
--         ON DELETE CASCADE
-- );

-- CREATE TABLE comments (
--     id INT NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,
--     blogId INT NOT NULL,
--     body VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id),
--     FOREIGN KEY (creatorId)
--         REFERENCES accounts (id)
--         ON DELETE CASCADE,
--     FOREIGN KEY (blogId)
--         REFERENCES blogs (id)
--         ON DELETE CASCADE
-- )

-- SELECT * FROM accounts
-- SELECT * FROM blogs
-- SELECT * FROM comments

-- DELETE FROM accounts
-- DELETE FROM blogs
-- DELETE FROM comments

-- DROP TABLE accounts
-- DROP TABLE blogs
-- DROP TABLE comments
