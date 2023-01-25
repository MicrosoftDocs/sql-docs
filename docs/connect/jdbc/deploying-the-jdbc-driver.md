---
title: Deploying the JDBC driver
description: Learn how you can redistribute and deploy the Microsoft JDBC Driver for SQL Server with your application and what files are required.
author: David-Engel
ms.author: v-davidengel
ms.date: 07/31/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
ms.custom: intro-deployment
---
# Deploying the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you deploy an application that depends on the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you must redistribute the JDBC driver together with your application. Unlike Windows Data Access Components (Windows DAC), which is a component of the Windows operating system, the JDBC driver is considered to be a component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
There are two approaches to deploying the JDBC driver with your application. One is to include the JDBC driver files as part of your own custom installation package. The second approach involves using the JDBC installation package provided by Microsoft, which you can download from the [Microsoft JDBC Driver for SQL Server download page](download-microsoft-jdbc-driver-for-sql-server.md).  
  
The following sections discuss how to use the JDBC installation package on Windows and UNIX operating systems.  
  
> [!NOTE]  
> For information about deploying Java applications in general, see the Java website.  
  
## Deploying the JDBC driver on Windows systems

When you deploy the JDBC driver on Windows operating systems, you must unpack the zipped installation package, which is typically named `sqljdbc_<version>_<language>.zip`.

## Deploying the driver on UNIX systems

When you deploy the JDBC driver on UNIX operating systems, you must use the gzip file version of the installation package, which is typically named `sqljdbc_<version>_<language>.tar.gz`.  
  
Before you install the JDBC driver, make sure that both the gzip and tar utilities are installed on the user's system, and that the folders that contain the executables for both utilities are added to the PATH environment variable.  
  
To unpack the zipped tar file, navigate to the directory where you want the driver unpacked and type the following command:  
  
`gzip -d sqljdbc_<version>_<language>.tar.gz`  
  
To unpack the tar file, move it to the directory where you want the driver installed and type the following command:  
  
`tar -xf sqljdbc_<version>_<language>.tar`  

## Legalities of driver redistribution

The JDBC Driver versions 6.0, 6.2, 6.4, 7.0, 7.2, 7.4, 8.2, and 8.4 are redistributable. Review the _Distributable Code_ clause in the license agreements.

The JDBC Driver versions 4.x are old and obsolete. Support for 4.x expired before 2018.

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)  
