--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `account_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(25) NOT NULL,
  `password` varchar(100) NOT NULL,
  `gender` enum('M','F','S','') NOT NULL,
  `email` varchar(150) NOT NULL,
  `group_id` int(11) NOT NULL,
  `slots` tinyint(3) NOT NULL DEFAULT 1,
  `pincode` varchar(4) DEFAULT NULL,
  `status` tinyint(2) NOT NULL DEFAULT 1,
  `created_at` datetime DEFAULT current_timestamp(),
  `updated_at` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  PRIMARY KEY (`account_id`),
  UNIQUE KEY `accounts_un` (`username`),
  KEY `accounts_FK` (`group_id`),
  CONSTRAINT `accounts_FK` FOREIGN KEY (`group_id`) REFERENCES `groups` (`group_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Table structure for table `characters`
--

DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `character_id` int(11) NOT NULL AUTO_INCREMENT,
  `account_id` int(11) NOT NULL,
  `position` tinyint(1) NOT NULL,
  `name` varchar(50) NOT NULL,
  `class_id` int(11) NOT NULL,
  `base_level` smallint(5) unsigned NOT NULL DEFAULT 1,
  `job_level` smallint(5) unsigned NOT NULL DEFAULT 1,
  `base_exp` bigint(20) NOT NULL DEFAULT 0,
  `job_exp` bigint(20) NOT NULL DEFAULT 0,
  `zeny` decimal(14,2) NOT NULL DEFAULT 0.00,
  `is_online` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`character_id`),
  KEY `characters_FK` (`account_id`),
  KEY `characters_FK_1` (`class_id`),
  CONSTRAINT `characters_FK` FOREIGN KEY (`account_id`) REFERENCES `accounts` (`account_id`),
  CONSTRAINT `characters_FK_1` FOREIGN KEY (`class_id`) REFERENCES `classes` (`class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Table structure for table `classes`
--

DROP TABLE IF EXISTS `classes`;
CREATE TABLE `classes` (
  `class_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Table structure for table `groups`
--

DROP TABLE IF EXISTS `groups`;
CREATE TABLE `groups` (
  `group_id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `status` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime DEFAULT current_timestamp(),
  `updated_at` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  PRIMARY KEY (`group_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
