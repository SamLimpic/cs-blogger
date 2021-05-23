/* CREATE TABLE profiles (
    id VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    picture VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,

    PRIMARY KEY (id)
); */

/* CREATE TABLE blogs (
    id INT NOT NULL AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    body VARCHAR(255) NOT NULL,
    imgUrl VARCHAR(255) NOT NULL,
    published TINYINT NOT NULL,

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId)
        REFERENCES profiles (id)
        ON DELETE CASCADE
); */

 /* CREATE TABLE comments (
     id INT NOT NULL AUTO_INCREMENT,
     creatorId VARCHAR(255) NOT NULL,
     blog INT NOT NULL,
     body VARCHAR(255) NOT NULL,

     PRIMARY KEY (id),

     FOREIGN KEY (blog)
         REFERENCES blogs (id)
         ON DELETE CASCADE,

     FOREIGN KEY (creatorId)
         REFERENCES profiles (id)
         ON DELETE CASCADE

 ) */

 /* SELECT * FROM profiles */
 /* SELECT * FROM blogs */
 /* SELECT * FROM comments */

 /* DELETE FROM profiles */
 /* DELETE FROM blogs */
 /* DELETE FROM comments */

 /* DROP TABLE profiles */
 /* DROP TABLE blogs */
 /* DROP TABLE comments */
