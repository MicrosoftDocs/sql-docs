---
title: "Non-system locale settings"
description: "Learn how different locale settings in Linux and macOS affect the Microsoft Drivers for PHP for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "locale, linux, macOS, system"
---

# Non-System Locale Settings
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This section only applies to Linux and macOS users, in particular those who deal with more than one locale in their php applications.

By default, [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] takes the `LC_ALL` environment variable defined in the system and overrides all other `LC_xxx` categories (perhaps except `$LANG` or `$LANGUAGE` under some circumstances), affecting the thousands separator, decimal point character, character set, month, day names, application messages like error messages, currency symbol, etc.

Starting with version 5.8.0, users may configure the localization settings using the php.ini file, as shown in the examples below.

## Set locale info using the SQLSRV Driver  
Add the following at the end of your php.ini file:
  
```  
[sqlsrv]  
sqlsrv.SetLocaleInfo = <option>
```  
  
## Set locale info using the PDO_SQLSRV Driver  
Add the following at the end of your php.ini file:
  
```  
[pdo_sqlsrv]  
pdo_sqlsrv.set_locale_info = <option>
```  
  
The **option** can be one of the following values:  
  
|Option|Description of behavior|
|---------|---------------|
|0|The driver ignores the system locale settings.|
|1|The driver reads the LC_CTYPE variable.|
|2|The driver reads the LC_ALL variable (this is the default).|
  

The `LC_CTYPE` category determines character handling rules, which govern the interpretation of sequences of bytes of text data characters, the classification of characters, and the behavior of character classes. It controls recognition of upper and lower case, alphabetic and non-alphabetic characters, and so on.

### Explanation

1. Option 0 -- use this when you do not want to alter the application locale.

1. Option 1 -- use this to set only the system value of `LC_CTYPE` without affecting the other `LC_xxx` categories.

1. Option 2 -- use `LC_ALL` to override all `LC_xxx` categories, affecting the php application and its extensions.

If the locale for any php script is not the same as the system ones, you may need to specify the locale in the php script(s) by calling the php built-in function [setlocale](https://www.php.net/manual/en/function.setlocale.php). 

For example, if the system default is `en_US.UTF-8` but the php script uses `de_DE.UTF-8`, then call the php function `setlocale()` appropriately.

For Option 2, indicate the desired locale in your php scripts only if it is different from the `LC_ALL` variable.

> [!NOTE]
> If nothing is defined in php.ini, the current default is to override all the other locale settings based on `LC_ALL`, which will be **deprecated**. In the near future, the default will be to ignore the system locale settings. Thus, users will need to modify the php.ini file accordingly if they wish to keep the current behavior.

If both sqlsrv and pdo_sqlsrv drivers are enabled, setting different options for the two drivers is not recommended.
