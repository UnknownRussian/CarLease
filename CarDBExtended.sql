-- Drop db if exists, to start from scratch
DROP DATABASE IF EXISTS extendedcardb;
CREATE DATABASE extendedcardb;
USE extendedcardb;

-- Drop tables if they exist (for clean rerun)
DROP TABLE IF EXISTS vehicle;
DROP TABLE IF EXISTS veh_reg;
DROP TABLE IF EXISTS login;
DROP TABLE IF EXISTS company;
DROP TABLE IF EXISTS user;

-- =====================
-- User Table
-- =====================
CREATE TABLE user (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name NVARCHAR(30) NOT NULL,
    lastname NVARCHAR(30) NOT NULL,
    address NVARCHAR(30) NOT NULL,
    zip_code NVARCHAR(5) NOT NULL,
    city NVARCHAR(30) NOT NULL,
    role NVARCHAR(15) NOT NULL
);

-- =====================
-- Login Table
-- =====================
CREATE TABLE login (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username NVARCHAR(30) NOT NULL UNIQUE,
    hash LONGTEXT NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE
);

-- =====================
-- Company Table
-- =====================
CREATE TABLE company (
    id INT AUTO_INCREMENT PRIMARY KEY,
    owner_id INT NOT NULL,
    name NVARCHAR(30) NOT NULL,
    address NVARCHAR(30) NOT NULL,
    zip_code NVARCHAR(5) NOT NULL,
    city NVARCHAR(30) NOT NULL,
    FOREIGN KEY (owner_id) REFERENCES user(id) ON DELETE CASCADE
);

-- =====================
-- Vehicle Registration Table
-- =====================
CREATE TABLE veh_reg (
    reg_num INT PRIMARY KEY,
    owner_id INT,
    owner_company_id INT,
    FOREIGN KEY (owner_id) REFERENCES user(id),
    FOREIGN KEY (owner_company_id) REFERENCES company(id)
);

-- =====================
-- Vehicle Table
-- =====================
CREATE TABLE vehicle (
    id INT AUTO_INCREMENT PRIMARY KEY,
    reg_num INT NOT NULL,
    manufacture NVARCHAR(30) NOT NULL,
    model NVARCHAR(30) NOT NULL,
    type NVARCHAR(30) NOT NULL,
    doors INT NOT NULL,
    color NVARCHAR(30) NOT NULL,
    gears INT NOT NULL,
    topspeed INT NOT NULL,
    hp INT NOT NULL,
    acceleration INT NOT NULL,
    model_year NVARCHAR(4) NOT NULL,
    fuel_type NVARCHAR(30) NOT NULL,
    price INT NOT NULL,
    FOREIGN KEY (reg_num) REFERENCES veh_reg(reg_num)
);

-- =====================
-- DEMO DATA
-- =====================

-- Insert Users
INSERT INTO user (name, lastname, address, zip_code, city, role) VALUES
('John', 'Doe', '123 Main St', '10001', 'New York', 'customer'),
('Alice', 'Smith', '456 Oak St', '90001', 'Los Angeles', 'dealer'),
('Bob', 'Brown', '789 Pine St', '60601', 'Chicago', 'admin');

-- Insert Logins
INSERT INTO login (username, hash, user_id) VALUES
('johndoe', 'hashed_password_1', 1),
('alicesmith', 'hashed_password_2', 2),
('bobbrown', 'hashed_password_3', 3);

-- Insert Companies
INSERT INTO company (owner_id, name, address, zip_code, city) VALUES
(2, 'AutoWorld', '321 Dealer Rd', '90001', 'Los Angeles'),
(3, 'CarHub', '654 Market St', '60601', 'Chicago');

-- Insert Vehicle Registrations
INSERT INTO veh_reg (reg_num, owner_id, owner_company_id) VALUES
(10001, 1, NULL),   -- John owns this car
(10002, NULL, 1),   -- AutoWorld owns this car
(10003, NULL, 2);   -- CarHub owns this car

-- Insert Vehicles
INSERT INTO vehicle (reg_num, manufacture, model, type, doors, color, gears, topspeed, hp, acceleration, model_year, fuel_type, price) VALUES
(10001, 'Toyota', 'Camry', 'Sedan', 4, 'Blue', 6, 210, 203, 8, '2020', 'Petrol', 24000),
(10002, 'Tesla', 'Model 3', 'Sedan', 4, 'White', 1, 225, 283, 5, '2021', 'Electric', 45000),
(10003, 'Ford', 'F-150', 'Truck', 2, 'Red', 10, 180, 400, 7, '2019', 'Diesel', 35000);

-- To disable safe update mode in MySQL
SET SQL_SAFE_UPDATES = 0;
/*
User Table
*/
-- CREATE
INSERT INTO user (name, lastname, address, zip_code, city, role)
VALUES ('Emma', 'Wilson', '987 River Rd', '30301', 'Atlanta', 'customer');

-- READ
SELECT * FROM user;

-- UPDATE
UPDATE user
SET city = 'Miami', zip_code = '33101'
WHERE id = 1;

-- DELETE
DELETE FROM user WHERE id = 4; -- deletes Emma

/*
Login Table
*/
-- CREATE
INSERT INTO login (username, hash, user_id)
VALUES ('emmawilson', 'hashed_password_4', 1);

-- READ
SELECT * FROM login;

-- UPDATE
UPDATE login
SET hash = 'new_hashed_password'
WHERE username = 'johndoe';

-- DELETE
DELETE FROM login WHERE username = 'emmawilson';

/*
Company Table
*/
-- CREATE
INSERT INTO company (owner_id, name, address, zip_code, city)
VALUES (2, 'DriveMax', '101 Auto Blvd', '73301', 'Austin');

-- READ
SELECT * FROM company;

-- UPDATE
UPDATE company
SET city = 'Dallas'
WHERE name = 'DriveMax';

-- DELETE
DELETE FROM company WHERE name = 'DriveMax';

/*
Vehicle Registration
*/
-- CREATE
INSERT INTO veh_reg (reg_num, owner_id, owner_company_id)
VALUES (10004, 1, NULL); -- John’s second car

-- READ
SELECT * FROM veh_reg;

-- UPDATE
UPDATE veh_reg
SET owner_company_id = 2, owner_id = NULL
WHERE reg_num = 10004; -- reassign to CarHub

-- DELETE
DELETE FROM veh_reg WHERE reg_num = 10004;

/*
Vehicle Table
*/
-- CREATE
INSERT INTO vehicle (reg_num, manufacture, model, type, doors, color, gears, topspeed, hp, acceleration, model_year, fuel_type, price)
VALUES (10001, 'Honda', 'Civic', 'Sedan', 4, 'Black', 6, 220, 180, 9, '2022', 'Petrol', 22000);

-- READ
SELECT * FROM vehicle;

-- UPDATE
UPDATE vehicle
SET color = 'Silver', price = 21000
WHERE model = 'Civic';

-- DELETE
DELETE FROM vehicle WHERE model = 'Civic';

-- To enable safe update mode in MySQL
SET SQL_SAFE_UPDATES = 1;

/*
Tests
*/

SELECT u.name, u.lastname, v.manufacture, v.model, v.color, v.model_year
FROM user u
JOIN veh_reg vr ON u.id = vr.owner_id
JOIN vehicle v ON v.reg_num = vr.reg_num;

SELECT c.name AS company, v.manufacture, v.model, v.type, v.price
FROM company c
JOIN veh_reg vr ON c.id = vr.owner_company_id
JOIN vehicle v ON v.reg_num = vr.reg_num;

SELECT vr.reg_num, v.model, 
       u.name AS user_owner, c.name AS company_owner
FROM veh_reg vr
LEFT JOIN user u ON vr.owner_id = u.id
LEFT JOIN company c ON vr.owner_company_id = c.id
JOIN vehicle v ON v.reg_num = vr.reg_num
WHERE vr.reg_num = 10002;

SELECT fuel_type, COUNT(*) AS total
FROM vehicle
GROUP BY fuel_type;

SELECT u.name, u.lastname, l.username
FROM user u
JOIN login l ON u.id = l.user_id;