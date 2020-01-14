---
title: "Non-System Locale Settings | Microsoft Docs"
ms.custom: ""
ms.date: "01/31/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "locale, linux, macOS, system"
author: "yitam"
ms.author: "v-yitam"
manager: v-mabarw
---

# Logging Activity
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Discussions in this section only apply to Linux and macOS users, in particular those who handle more than one locale in their php applications.

By default, [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] takes `LC_ALL` the environment variable defined in the system and overrides all the other `LC_xxx` categories (perhaps except `$LANG` or `$LANGUAGE` under some circumstances), affecting the thousand separator, decimal point character, character set, month, day names, application messages like error messages, currency symbol, etc..

Starting with version 5.8.0, the users may configure the drivers to not override the localization settings by using the php.ini file. 

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
|0|Drivers ignore the system locale settings and do nothing.|
|1|Drivers reads LC_CTYPE variable.|
|2|Drivers reads LC_ALL variable (this is the default if nothing is defined in php.ini).|
  

The `LC_CTYPE` category determines character handling rules, which govern the interpretation of sequences of bytes of text data characters, the classification of characters, and the behavior of character classes. In short, it controls recognition of upper and lower case, alphabetic or non-alphabetic characters, and so on.

### Explanation

1. Option 0 -- use this when you do not want to alter the application locale.

1. Option 1 -- use this as one step up from Option 0, which only sets the system value of `LC_CTYPE` without affecting the other `LC_xxx` categories.

1. Option 2 -- use `LC_ALL` to override all `LC_xxx` categories, affecting the php application and its extensions.

If the locale for any php script is not the same as the system ones, the users may be required to specify the locale in the php script(s) by calling the php built-in function [setlocale](https://www.php.net/manual/en/function.setlocale.php). 

For example, if the system default is `en_US.UTF-8` but the user wants the php script to use `de_DE.UTF-8` in some cases, then call the php function `setlocale()` appropriately.

For Option 2, the users will indicate their desired locale in their php scripts only if it is different from the `LC_ALL` variable.

> [!NOTE]
> If nothing is defined in php.ini, the current default is to override all the other locale
> settings based on `LC_ALL`, which will be **deprecated**. In the near future, the default is
> to ignore the system locale settings. Thus, the users are required to modify the php.ini file
> accordingly, if they desire to keep the current behavior.

If both sqlsrv and pdo_sqlsrv drivers are enabled, setting different options for the two drivers is not recommended.