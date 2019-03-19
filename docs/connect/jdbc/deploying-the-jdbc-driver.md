---
title: "Deploying the JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 3ad3508d-d9b1-47fb-a63b-21cdc3ed44e0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Deploying the JDBC Driver
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  When you deploy an application that depends on the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you must redistribute the JDBC driver together with your application. Unlike Windows Data Access Components (Windows DAC), which is a component of the Windows operating system, the JDBC driver is considered to be a component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 There are two approaches to deploying the JDBC driver with your application. One is to include the JDBC driver files as part of your own custom installation package. The second approach involves using the JDBC installation package provided by Microsoft, which you can download from the [Microsoft JDBC Driver for SQL Server Developer Center](https://go.microsoft.com/fwlink/?LinkId=70166).  
  
 The following sections discuss how to use the JDBC installation package on Windows and UNIX operating systems.  
  
> [!NOTE]  
>  For information about deploying Java applications in general, see the Java website.  
  
## Deploying the JDBC Driver on Windows Systems  
 When you deploy the JDBC driver on Windows operating systems, you must use the executable zip file version of the installation package, which is typically named `sqljdbc_<version>_<language>.exe`.  
  
 To run the executable zip file silently, you must use the `/auto` command-line option on the command line or in a batch file as in the following:  
  
 `sqljdbc_<version>_<language>.exe /auto`  
  
> [!NOTE]  
>  When you use the `/auto` option it is not a truly silent installation, as a WinZip dialog box still appears on the user's screen. However, you will not need to interact with it and it closes as soon as the unzip operation is complete.  
  
## Deploying the Driver on UNIX Systems  
 When you deploy the JDBC driver on UNIX operating systems, you must use the gzip file version of the installation package, which is typically named `sqljdbc_<version>_<language>.tar.gz`.  
  
 Before you install the JDBC driver, make sure that both the gzip and tar utilities are installed on the user's system, and that the folders that contain the executables for both utilities are added to the PATH environment variable.  
  
 To unpack the zipped tar file, navigate to the directory where you want the driver unpacked and type the following command:  
  
 `gzip -d sqljdbc_<version>_<language>.tar.gz`  
  
 To unpack the tar file, move it to the directory where you want the driver installed and type the following command:  
  
 `tar -xf sqljdbc_<version>_<language>.tar`  
  
## See Also  
 [Overview of the JDBC Driver](../../connect/jdbc/overview-of-the-jdbc-driver.md)  
  
  
