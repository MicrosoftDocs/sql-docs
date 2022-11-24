---
description: "ConfigDSN Function"
title: "ConfigDSN Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "ConfigDSN"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "ConfigDSN"
helpviewer_keywords: 
  - "ConfigDSN [ODBC]"
ms.assetid: 01ced74e-c575-4a25-83f5-bd7d918123f8
author: David-Engel
ms.author: v-davidengel
---
# ConfigDSN Function
**Conformance**  
 Version Introduced: ODBC 1.0  
  
 **Summary**  
 **ConfigDSN** adds, modifies, or deletes data sources from the system information. It may prompt the user for connection information. It can be in the driver DLL or a separate setup DLL.  
  
## Syntax  
  
```cpp  
  
BOOL ConfigDSN(  
     HWND     hwndParent,  
     WORD     fRequest,  
     LPCSTR   lpszDriver,  
     LPCSTR   lpszAttributes);  
```  
  
## Arguments  
 *hwndParent*  
 [Input] Parent window handle. The function will not display any dialog boxes if the handle is null.  
  
 *fRequest*  
 [Input] Type of request. The *fRequest* argument must contain one of the following values:  
  
 ODBC_ADD_DSN: Add a new data source.  
  
 ODBC_CONFIG_DSN: Configure (modify) an existing data source.  
  
 ODBC_REMOVE_DSN: Remove an existing data source.  
  
 *lpszDriver*  
 [Input] Driver description (usually the name of the associated DBMS) presented to users instead of the physical driver name.  
  
 *lpszAttributes*  
 [Input] A doubly null-terminated list of attributes in the form of keyword-value pairs. For more information, see "Comments."  
  
## Returns  
 The function returns TRUE if it is successful, FALSE if it fails.  
  
## Diagnostics  
 When **ConfigDSN** returns FALSE, an associated *\*pfErrorCode* value is posted to the installer error buffer by a call to **SQLPostInstallerError** and can be obtained by calling **SQLInstallerError**. The following table lists the *\*pfErrorCode* values that can be returned by **SQLInstallerError** and explains each one in the context of this function.  
  
|*\*pfErrorCode*|Error|Description|  
|---------------------|-----------|-----------------|  
|ODBC_ERROR_INVALID_HWND|Invalid window handle|The *hwndParent* argument was invalid.|  
|ODBC_ERROR_INVALID_KEYWORD_VALUE|Invalid keyword-value pairs|The *lpszAttributes* argument contained a syntax error.|  
|ODBC_ERROR_INVALID_NAME|Invalid driver or translator name|The *lpszDriver* argument was invalid. It could not be found in the registry.|  
|ODBC_ERROR_INVALID_REQUEST_TYPE|Invalid type of request|The *fRequest* argument was not one of the following:<br /><br /> ODBC_ADD_DSN ODBC_CONFIG_DSN ODBC_REMOVE_DSN|  
|ODBC_ERROR_REQUEST_FAILED|*Request* failed|Could not perform the operation requested by the *fRequest* argument.|  
|ODBC_ERROR_DRIVER_SPECIFIC|Driver- or translator-specific error|A driver-specific error for which there is no defined ODBC installer error. The *SzError* argument in a call to the **SQLPostInstallerError** function should contain the driver-specific error message.|  
  
## Comments  
 **ConfigDSN** receives connection information from the installer DLL as a list of attributes in the form of keyword-value pairs. Each pair is terminated with a null byte, and the entire list is terminated with a null byte. (That is, two null bytes mark the end of the list.) Spaces are not allowed around the equal sign in the keyword-value pair. **ConfigDSN** can accept keywords that are not valid keywords for **SQLBrowseConnect** and **SQLDriverConnect**. **ConfigDSN** does not necessarily support all keywords that are valid keywords for **SQLBrowseConnect** and **SQLDriverConnect**. (**ConfigDSN** does not accept the **DRIVER** keyword.) The keywords used by the **ConfigDSN** function must support all the options required to re-create the data source using the AUTO setup feature of the installer. When the uses of the **ConfigDSN** values and the connection string values are the same, the same keywords should be used.  
  
 As in **SQLBrowseConnect** and **SQLDriverConnect**, the keywords and their values should not contain the **[]{}(),;?\*=!@** characters, and the value of the **DSN** keyword cannot consist only of blanks. Because of the registry grammar, keywords and data source names cannot contain the backslash (\\) character.  
  
 **ConfigDSN** should call **SQLValidDSN** to check the length of the data source name and to verify that no invalid characters are included in the name. If the data source name is longer than SQL_MAX_DSN_LENGTH or includes invalid characters, **SQLValidDSN** returns an error and **ConfigDSN** returns an error. The length of the data source name is also checked by **SQLWriteDSNToIni**.  
  
 For example, to configure a data source that requires a user ID, password, and database name, a setup application might pass the following keyword-value pairs:  
  
```  
DSN=Personnel Data\0UID=Smith\0PWD=Sesame\0DATABASE=Personnel\0\0  
```  
  
 For more information about these keywords, see [SQLDriverConnect](../../../odbc/reference/syntax/sqldriverconnect-function.md) and each driver's documentation.  
  
 To display a dialog box, *hwndParent* must not be null.  
  
## Adding a Data Source  
 If a data source name is passed to **ConfigDSN** in *lpszAttributes*, **ConfigDSN** checks that the name is valid. If the data source name matches an existing data source name and *hwndParent* is null, **ConfigDSN** overwrites the existing name. If it matches an existing name and *hwndParent* is not null, **ConfigDSN** prompts the user to overwrite the existing name.  
  
 If *lpszAttributes* contains enough information to connect to a data source, **ConfigDSN** can add the data source or display a dialog box with which the user can change the connection information. If *lpszAttributes* does not contain enough information to connect to a data source, **ConfigDSN** must determine the necessary information; if *hwndParent* is not null, it displays a dialog box to retrieve the information from the user.  
  
 If **ConfigDSN** displays a dialog box, it must display any connection information passed to it in *lpszAttributes*. In particular, if a data source name was passed to it, **ConfigDSN** displays that name but does not allow the user to change it. **ConfigDSN** can supply default values for connection information not passed to it in *lpszAttributes*.  
  
 If **ConfigDSN** cannot get complete connection information for a data source, it returns FALSE.  
  
 If **ConfigDSN** can get complete connection information for a data source, it calls **SQLWriteDSNToIni** in the installer DLL to add the new data source specification to the Odbc.ini file (or registry). **SQLWriteDSNToIni** adds the data source name to the [ODBC Data Sources] section, creates the data source specification section, and adds the **DRIVER** keyword with the driver description as its value. **ConfigDSN** calls **SQLWritePrivateProfileString** in the installer DLL to add any additional keywords and values used by the driver.  
  
## Modifying a Data Source  
 To modify a data source, a data source name must be passed to **ConfigDSN** in *lpszAttributes*. **ConfigDSN** checks that the data source name is in the Odbc.ini file (or registry).  
  
 If *hwndParent* is null, **ConfigDSN** uses the information in *lpszAttributes* to modify the information in the Odbc.ini file (or registry). If *hwndParent* is not null, **ConfigDSN** displays a dialog box using the information in *lpszAttributes*; for information not in *lpszAttributes*, it uses information from the system information. The user can modify the information before **ConfigDSN** stores it in the system information.  
  
 If the data source name was changed, **ConfigDSN** first calls **SQLRemoveDSNFromIni** in the installer DLL to remove the existing data source specification from the Odbc.ini file (or registry). It then follows the steps in the preceding section to add the new data source specification. If the data source name was not changed, **ConfigDSN** calls **SQLWritePrivateProfileString** in the installer DLL to make any other changes. **ConfigDSN** may not delete or change the value of the **Driver** keyword.  
  
## Deleting a Data Source  
 To delete a data source, a data source name must be passed to **ConfigDSN** in *lpszAttributes*. **ConfigDSN** checks that the data source name is in the Odbc.ini file (or registry). It then calls **SQLRemoveDSNFromIni** in the installer DLL to remove the data source.  
  
## Note
 If writing a Unicode version of this routine, it must be called **ConfigDSNW**, with LPCWSTR arguments instead of LPCSTR.
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Adding, modifying, or removing a data source|[SQLConfigDataSource](../../../odbc/reference/syntax/sqlconfigdatasource-function.md)|  
|Getting a value from the Odbc.ini file or the registry|[SQLGetPrivateProfileString](../../../odbc/reference/syntax/sqlgetprivateprofilestring-function.md)|  
|Removing the default data source|[SQLRemoveDefaultDataSource](../../../odbc/reference/syntax/sqlremovedefaultdatasource-function.md)|  
|Removing a data source name from Odbc.ini (or registry)|[SQLRemoveDSNFromIni](../../../odbc/reference/syntax/sqlremovedsnfromini-function.md)|  
|Adding a data source name to Odbc.ini (or registry)|[SQLWriteDSNToIni](../../../odbc/reference/syntax/sqlwritedsntoini-function.md)|  
|Writing a value to the Odbc.ini file or the registry|[SQLWritePrivateProfileString](../../../odbc/reference/syntax/sqlwriteprivateprofilestring-function.md)|
