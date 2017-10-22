SELECT * FROM testdb.discussgrouplink;CREATE TABLE `discussgrouplink` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `OldLink` varchar(45) NOT NULL,
  `NewLink` varchar(45) DEFAULT NULL,
  `IsComputed` tinyint(1) NOT NULL DEFAULT '0',
  `CreateTime` datetime NOT NULL,
  `ChangeTime` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `ID_UNIQUE` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
