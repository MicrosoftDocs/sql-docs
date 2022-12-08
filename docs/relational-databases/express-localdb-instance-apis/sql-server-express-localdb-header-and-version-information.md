---
description: "SQL Server Express LocalDB Header and Version Information"
title: "SQL Server Express LocalDB header & version information"
ms.date: 11/10/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: "reference"
apilocation: 
  - "sqluserinstance.dll"
author: markingmyname
ms.author: maghan
ms.custom: seo-dt-2019
---
# SQL Server Express LocalDB Header and Version Information
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  There is no separate header file for the SQL Server Express LocalDB instance API; the LocalDB function signatures and error codes are defined in the Microsoft OLE DB Driver for SQL Server header file (msoledbsql.h). To use the LocalDB instance API, you must include the msoledbsql.h header file in your project. This doc has recently been updated and no longer references the SQL Server Native Client header file (sqlncli.h).
  
## LocalDB Versioning  
 The LocalDB installation uses a single set of binaries per major SQL Server version. These LocalDB versions are maintained and patched independently. This means that the user has to specify which LocalDB baseline release (that is, major SQL Server version) he or she will be using. The version is specified in the standard version format defined by the .NET Framework **System.Version** class:  
  
 *major.minor[.build[.revision]]*  
  
 The first two numbers in the version string (*major* and *minor*) are mandatory. The last two numbers in the version string (*build* and *revision*) are optional and default to zero if the user leaves them out. This means that if the user specifies only "12.2" as the LocalDB version number, it will be treated as if the user specified "12.2.0.0".  
  
 The version for the LocalDB installation is defined in the `MSSQLServer\CurrentVersion` registry key under the SQL Server instance registry key, for example:  
  
```  
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL13E.LOCALDB\ MSSQLServer\CurrentVersion: "CurrentVersion"="12.0.2531.0"  
```  
  
 Multiple LocalDB versions on the same workstation are supported side-by-side. However, user code always uses the latest available **SQLUserInstance** DLL on the local computer to connect to LocalDB instances.  
  
## Locating the SQLUserInstance DLL  
 To locate the **SQLUserInstance** DLL, the client provider uses the following registry key:  
  
```  
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions]  
```  
  
 Under this key there is a list of keys, one for each version of LocalDB installed on the computer. Each of these keys is named with the LocalDB version number in the format *\<major-version>*.*\<minor-version>* (for example, the key for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] is named 13.0). Under each version key there is an `InstanceAPIPath` name-value pair that defines the full path to the SQLUserInstance.dll file installed with that version. The following example shows the registry entries for a computer that has LocalDB versions 11.0 and 13.0 installed:  
  
```  
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions\13.0]  
"InstanceAPIPath"="C:\\Program Files\\Microsoft SQL Server\\130\\LocalDB\\Binn\\SqlUserInstance.dll"  
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions\13.0]  
"InstanceAPIPath"="C:\\Program Files\\Microsoft SQL Server\\130\\LocalDB\\Binn\\SqlUserInstance.dll"]  
```  
  
 The client provider must find the latest version among all installed versions and load the **SQLUserInstance** DLL file from the associated `InstanceAPIPath` value.  
  
### WOW64 Mode on 64-bit Windows  
 64-bit installations of LocalDB will have an additional set of registry keys to allow 32-bit applications running in Windows-32-on-Windows-64 (WOW64) mode to use LocalDB. Specifically, on 64-bit Windows, the LocalDB MSI will create the following registry keys:  
  
```  
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Wow6432Node\Microsoft SQL Server Local DB\Installed Versions\13.0]  
"InstanceAPIPath"="C:\\Program Files (x86)\\Microsoft SQL Server\\130\\LocalDB\\Binn\\SqlUserInstance.dll"  
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Wow6432Node\Microsoft SQL Server Local DB\Installed Versions\13.0]  
"InstanceAPIPath"="C:\\Program Files (x86)\\Microsoft SQL Server\\130\\LocalDB\\Binn\\SqlUserInstance.dll"]  
  
```  
  
 64-bit programs reading the `Installed Versions` key will see values pointing to 64-bit versions of the **SQLUserInstance** DLL, while 32-bit programs (running on 64-bit Windows in WOW64 mode) will be automatically redirected to an `Installed Versions` key located under the `Wow6432Node` hive. This key contains values pointing to 32-bit versions of the **SQLUserInstance** DLL.  
  
## Using LOCALDB_DEFINE_PROXY_FUNCTIONS  
 The LocalDB instance API defines a constant named LOCALDB_DEFINE_PROXY_FUNCTIONS that automates the discovery and loading of the **SqlUserInstance** DLL.  
  
 The section of code enabled by this constant provides an implementation of proxies for each of the LocalDB APIs. The proxy implementations use a common function to bind to entry points in the latest installed **SqlUserInstance** DLL, and then forward the requests.  
  
 The proxy functions are enabled only if the constant LOCALDB_DEFINE_PROXY_FUNCTIONS is defined in the user code before including the msoledbsql.h file. The constant should be defined in only one source module (.cpp file) because it defines external function names for all of the API entry points. It provides an implementation of proxies for each of the LocalDB APIs.  
  
 The following code example shows how to use the macro from the native C++ code:  
  
```  
// Define the LOCALDB_DEFINE_PROXY_FUNCTIONS constant to enable the LocalDB proxy functions   
// The #define has to take place BEFORE the API header file (msoledbsql.h) is included  
#define LOCALDB_DEFINE_PROXY_FUNCTIONS  
#include <msoledbsql.h>  
...  
HRESULT hr = S_OK;  
  
// Create LocalDB instance by calling the create API proxy function included by macro  
if (FAILED(hr = LocalDBCreateInstance( L"12.0", L"name", 0)))  
{  
...  
}  
...  
  
```  
  
  
