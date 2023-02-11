DROP DATABASE IF EXISTS `fox`;
CREATE DATABASE IF NOT EXISTS fox CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `fox`;
CREATE TABLE `employee`
(
    `id` int PRIMARY KEY AUTO_INCREMENT,
    `parent_id` int  DEFAULT NULL,
    `position` varchar(255)  NOT NULL,
    `full_name` varchar(255) NOT NULL,
    INDEX (`full_name`),
    FOREIGN KEY (`parent_id`) REFERENCES `employee` (`id`) 
    ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `employee` values (1, null, 'CEO', 'Eliott Nieves');
INSERT INTO `employee` values (2, 1, 'Vice President', 'Harvey-Lee Travis');
INSERT INTO `employee` values (3, 1, 'Vice President', 'Gethin Morley');
INSERT INTO `employee` values (4, 1, 'Vice President', 'Sonnie Kim');
INSERT INTO `employee` values (5, 2, 'Manager', 'Dante Curtie');
INSERT INTO `employee` values (6, 2, 'Manager', 'Honey Mullen');
INSERT INTO `employee` values (7, 3, 'Manager', 'Steffen Taylor');
INSERT INTO `employee` values (8, 3, 'Manager', 'Charlton Hester');
INSERT INTO `employee` values (9, 3, 'Manager', 'Ishmeal Orr');
INSERT INTO `employee` values (10, 4, 'Manager', 'Kiara Johns');
INSERT INTO `employee` values (11, 4, 'Manager', 'Grant Cash');
INSERT INTO `employee` values (12, 9, 'Manager', 'Dua Frost');
INSERT INTO `employee` values (13, 9, 'Manager', 'Salahuddin Eastwood');
INSERT INTO `employee` values (14, 10, 'Employee', 'Juanita Cottrell');
INSERT INTO `employee` values (15, 10, 'Employee', 'Allana Frey');
INSERT INTO `employee` values (16, 12, 'Employee', 'Nicole Tapia');
INSERT INTO `employee` values (17, 12, 'Employee', 'Arisha Hinton');
INSERT INTO `employee` values (18, 12, 'Employee', 'Martha Morley');
INSERT INTO `employee` values (19, 12, 'Employee', 'Cathy Mcpherson');