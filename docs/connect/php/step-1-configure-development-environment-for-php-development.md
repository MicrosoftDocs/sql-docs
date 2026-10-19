---
title: "Step 1: Configure Environment for PHP"
description: Step 1 of this getting started guide involves installing PHP, the Microsoft ODBC Driver for SQL Server, and configuring your development environment.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 03/25/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ms.custom:
  - linux-related-content
---
# Step 1: Configure environment for PHP development

[!INCLUDE [Driver_PHP_Download](../../includes/driver_php_download.md)]

To connect to SQL Server from a PHP application, you need the following components installed in this order:

1. **PHP runtime**: Download and install PHP from [php.net](https://www.php.net/downloads.php) (Windows) or use your system package manager (Linux/macOS).

1. **Microsoft ODBC Driver for SQL Server**: The PHP drivers require the ODBC driver. Download it from [Download ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).

1. **Microsoft Drivers for PHP for SQL Server**: Download the drivers from [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md). For more information on version compatibility, see [System requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md).

1. **Web server** (optional): Configure IIS (Windows) or Apache/Nginx (Linux/macOS) if you're building web applications.

## Windows

1. [Download and install PHP](https://windows.php.net/download/) — use the **Thread Safe** (TS) version for IIS, or **Non-Thread Safe** (NTS) for Apache or CLI.
1. Download and install the [ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).
1. Download the [Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md) and extract the `.dll` files to your PHP extensions directory.
1. Enable the driver by adding the following lines to your `php.ini` file:

   ```ini
   extension=php_sqlsrv.dll
   extension=php_pdo_sqlsrv.dll
   ```

1. Verify the installation by running `php -m` and checking that `sqlsrv` and `pdo_sqlsrv` appear in the list.

For detailed instructions, see [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md). To configure IIS, see [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md).

## Linux and macOS

For step-by-step instructions on installing PHP, the ODBC driver, and the PHP drivers on Linux and macOS, see [Linux and macOS Installation Tutorial for the Microsoft Drivers for PHP for SQL Server](installation-tutorial-linux-mac.md).
