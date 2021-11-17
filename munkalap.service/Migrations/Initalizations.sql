CREATE DATABASE WorkSheet
	CHARACTER SET utf8
	COLLATE utf8_hungarian_ci;

use WorkSheet;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Employee` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Employee` PRIMARY KEY (`Id`)
);

CREATE TABLE `Failures` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Issuer` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `IssueTimeStamp` datetime(6) NOT NULL,
    `Room` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `AssignedId` int NULL,
    `AssignTimeStamp` datetime(6) NULL,
    `AssignComment` varchar(200) CHARACTER SET utf8mb4 NULL,
    `WorkStarted` datetime(6) NULL,
    `WorkFinished` datetime(6) NULL,
    `FinishComment` varchar(200) CHARACTER SET utf8mb4 NULL,
    `IsChecked` tinyint(1) NULL,
    CONSTRAINT `PK_Failures` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Failures_Employee_AssignedId` FOREIGN KEY (`AssignedId`) REFERENCES `Employee` (`Id`) ON DELETE RESTRICT
);

INSERT INTO `Employee` (`Id`, `IsDeleted`, `Name`)
VALUES (1, FALSE, 'Béla');

INSERT INTO `Employee` (`Id`, `IsDeleted`, `Name`)
VALUES (2, FALSE, 'Géza');

CREATE UNIQUE INDEX `IX_Employee_Name` ON `Employee` (`Name`);

CREATE INDEX `IX_Failures_AssignedId` ON `Failures` (`AssignedId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211005081822_Initialization', '3.1.19');

