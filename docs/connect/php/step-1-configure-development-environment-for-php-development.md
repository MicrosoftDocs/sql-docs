---
title: "Step 1: Configure environment for PHP"
description: Step 1 of this getting started guide involves installing PHP, the Microsoft ODBC Driver for SQL Server, and configuring your development environment.
author: David-Engel
ms.author: v-davidengel
ms.date: 03/26/2018
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Step 1: Configure environment for PHP development

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

- Identify which version of the PHP driver you will use, based on your environment, as noted here:  [System Requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)
- Download and install applicable PHP Driver here: [Download Microsoft PHP Driver](https://www.microsoft.com/download/details.aspx?id=20098)
- Download and install applicable ODBC Driver here:  [Download ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md)
- Configure the PHP driver and web server for your specific operating system:

## Windows

- Configure loading the PHP driver, as noted here: [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md)
- Configure IIS to host PHP applications, as noted here: [Configuring IIS for the Microsoft Drivers for PHP for SQL Server](configuring-iis-for-php-sql-driver.md)

## Linux and macOS

- Configure loading the PHP driver and configure your web server to host PHP applications, as noted here: [PHP Linux and macOS Drivers Installation Tutorial](installation-tutorial-linux-mac.md)
