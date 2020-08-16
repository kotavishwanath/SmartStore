# SmartStore

Run this query as well inoder to full fill the purcase API

One update in DB. Execute this query: 

ALTER TABLE `smartstore`.`userpurchasedproduct` 
CHANGE COLUMN `Id` `Id` VARCHAR(50) NOT NULL ;
