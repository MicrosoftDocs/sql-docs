---
title: "SQLDriverConnect Function | Microsoft Docs"
description: "The SQLDriverConnect function is part of the ODBC API standard and this reference documentation provides information on its syntax."
ms.custom: ""
ms.date: "08/20/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
apiname: 
  - "SQLDriverConnect"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLDriverConnect"
helpviewer_keywords: 
  - "SQLDriverConnect function [ODBC]"
ms.assetid: e299be1d-5c74-4ede-b6a3-430eb189134f
author: David-Engel
ms.author: v-davidengel
---
# SQLDriverConnect Function
**Conformance**  
 Version Introduced: ODBC 1.0 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLDriverConnect** is an alternative to **SQLConnect**. It supports data sources that require more connection information than the three arguments in **SQLConnect**, dialog boxes to prompt the user for all connection information, and data sources that are not defined in the system information. For more information, see [Connecting with SQLDriverConnect](../develop-app/connecting-with-sqldriverconnect.md).  

## Syntax  
  
```cpp  
  
SQLRETURN SQLDriverConnect(  
     SQLHDBC         ConnectionHandle,  
     SQLHWND         WindowHandle,  
     SQLCHAR *       InConnectionString,  
     SQLSMALLINT     StringLength1,  
     SQLCHAR *       OutConnectionString,  
     SQLSMALLINT     BufferLength,  
     SQLSMALLINT *   StringLength2Ptr,  
     SQLUSMALLINT    DriverCompletion);  
```  
  
## Arguments  
 *ConnectionHandle*  
 [Input] Connection handle.  
  
 *WindowHandle*  
 [Input] Window handle. The application can pass the handle of the parent window, if applicable, or a null pointer if either the window handle is not applicable or **SQLDriverConnect** will not present any dialog boxes.  
  
 *InConnectionString*  
 [Input] A full connection string (see the syntax in "Comments"), a partial connection string, or an empty string.  
  
 *StringLength1*  
 [Input] Length of **InConnectionString*, in characters if the string is Unicode, or bytes if string is ANSI or DBCS.  
  
 *OutConnectionString*  
 [Output] Pointer to a buffer for the completed connection string. Upon successful connection to the target data source, this buffer contains the completed connection string. Applications should allocate at least 1,024 characters for this buffer.  
  
 If *OutConnectionString* is NULL, *StringLength2Ptr* will still return the total number of characters (excluding the null-termination character for character data) available to return in the buffer pointed to by *OutConnectionString*.  
  
 *BufferLength*  
 [Input] Length of the **OutConnectionString* buffer, in characters.  
  
 *StringLength2Ptr*  
 [Output] Pointer to a buffer in which to return the total number of characters (excluding the null-termination character) available to return in \**OutConnectionString*. If the number of characters available to return is greater than or equal to *BufferLength*, the completed connection string in \**OutConnectionString* is truncated to *BufferLength* minus the length of a null-termination character.  
  
 *DriverCompletion*  
 [Input] Flag that indicates whether the Driver Manager or driver must prompt for more connection information:  
  
 SQL_DRIVER_PROMPT, SQL_DRIVER_COMPLETE,  SQL_DRIVER_COMPLETE_REQUIRED, or SQL_DRIVER_NOPROMPT.  
  
 (For additional information, see "Comments.")  
  
## Returns  
 SQL_SUCCESS, SQL_SUCCESS_WITH_INFO, SQL_NO_DATA, SQL_ERROR, SQL_INVALID_HANDLE, or SQL_STILL_EXECUTING.  
  
## Diagnostics  
 When **SQLDriverConnect** returns either SQL_ERROR or SQL_SUCCESS_WITH_INFO, an associated SQLSTATE value may be obtained by calling **SQLGetDiagRec** with an *fHandleType* of SQL_HANDLE_DBC and an *hHandle* of *ConnectionHandle*. The following table lists the SQLSTATE values commonly returned by **SQLDriverConnect** and explains each one in the context of this function; the notation "(DM)" precedes the descriptions of SQLSTATEs returned by the Driver Manager. The return code associated with each SQLSTATE value is SQL_ERROR, unless noted otherwise.  
  
|SQLSTATE|Error|Description|  
|--------------|-----------|-----------------|  
|01000|General warning|Driver-specific informational message. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01004|String data, right truncated|The buffer \**OutConnectionString* was not large enough to return the entire connection string, so the connection string was truncated. The length of the untruncated connection string is returned in **StringLength2Ptr*. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S00|Invalid connection string attribute|An invalid attribute keyword was specified in the connection string (*InConnectionString*), but the driver was able to connect to the data source anyway. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S02|Option value changed|The driver did not support the specified value pointed to by the *ValuePtr* argument in **SQLSetConnectAttr** and substituted a similar value. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S08|Error saving file DSN|The string in *\*InConnectionString* contained a **FILEDSN** keyword, but the .dsn file was not saved. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|01S09|Invalid keyword|(DM) The string in *\*InConnectionString* contained a **SAVEFILE** keyword but not a **DRIVER** or a **FILEDSN** keyword. (Function returns SQL_SUCCESS_WITH_INFO.)|  
|08001|Client unable to establish connection|The driver was unable to establish a connection with the data source.|  
|08002|Connection name in use|(DM) The specified *ConnectionHandle* had already been used to establish a connection with a data source, and the connection was still open.|  
|08004|Server rejected the connection|The data source rejected the establishment of the connection for implementation-defined reasons.|  
|08S01|Communication link failure|The communication link between the driver and the data source to which the driver was attempting to connect failed before the **SQLDriverConnect** function completed processing.|  
|28000|Invalid authorization specification|Either the user identifier or the authorization string, or both, as specified in the connection string (*InConnectionString*), violated restrictions defined by the data source.|  
|HY000|General error|An error occurred for which there was no specific SQLSTATE and for which no implementation-specific SQLSTATE was defined. The error message returned by **SQLGetDiagRec** in the *\*szMessageText* buffer describes the error and its cause.|  
|HY000|General error: Invalid file dsn|(DM) The string in **InConnectionString* contained a FILEDSN keyword, but the name of the .dsn file was not found.|  
|HY000|General error: Unable to create file buffer|(DM) The string in **InConnectionString* contained a FILEDSN keyword, but the .dsn file was unreadable.|  
|HY001|Memory allocation error|The Driver Manager was unable to allocate memory required to support execution or completion of the **SQLDriverConnect** function.<br /><br /> The driver was unable to allocate memory required to support execution or completion of the function.|  
|HY008|Operation canceled|Asynchronous processing was enabled for the *ConnectionHandle*. The function was called, and before it completed execution, the [SQLCancelHandle function](../../../odbc/reference/syntax/sqlcancelhandle-function.md) was called on the *ConnectionHandle*, and then the **SQLDriverConnect** function was called again on the *ConnectionHandle*.<br /><br /> Or, the **SQLDriverConnect** function was called, and before it completed execution, **SQLCancelHandle** was called on the *ConnectionHandle* from a different thread in a multithread application.|  
|HY010|Function sequence error|(DM) Another asynchronously executing function (not **SQLDriverConnect**) was called for the *ConnectionHandle* and was still executing when the **SQLDriverConnect** function was called.|  
|HY013|Memory management error|The **SQLDriverConnect** function call could not be processed because the underlying memory objects could not be accessed, possibly because of low memory conditions.|  
|HY090|Invalid string or buffer length|(DM) The value specified for argument *StringLength1* was less than 0 and was not equal to SQL_NTS.<br /><br /> (DM) The value specified for argument *BufferLength* was less than 0.|  
|HY092|Invalid attribute/option identifier|(DM) The *DriverCompletion* argument was SQL_DRIVER_PROMPT, and the *WindowHandle* argument was a null pointer.|  
|HY110|Invalid driver completion|(DM) The value specified for the argument *DriverCompletion* was not equal to SQL_DRIVER_PROMPT, SQL_DRIVER_COMPLETE, SQL_DRIVER_COMPLETE_REQUIRED, or SQL_DRIVER_NOPROMPT.<br /><br /> (DM) Connection pooling was enabled, and the value specified for the argument *DriverCompletion* was not equal to SQL_DRIVER_NOPROMPT.|  
|HYC00|Optional feature not implemented|The driver does not support the version of ODBC behavior that the application requested.|  
|HYT00|Timeout expired|The login timeout period expired before the connection to the data source completed. The timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_LOGIN_TIMEOUT.|  
|HYT01|Connection timeout expired|The connection timeout period expired before the data source responded to the request. The connection timeout period is set through **SQLSetConnectAttr**, SQL_ATTR_CONNECTION_TIMEOUT.|  
|IM001|Driver does not support this function|(DM) The driver corresponding to the specified data source name does not support the function.|  
|IM002|Data source not found and no default driver specified|(DM) The data source name specified in the connection string (*InConnectionString*) was not found in the system information, and there was no default driver specification.<br /><br /> (DM) ODBC data source and default driver information could not be found in the system information.|  
|IM003|Specified driver could not be loaded|(DM) The driver listed in the data source specification in the system information or specified by the **DRIVER** keyword was not found or could not be loaded for some other reason.|  
|IM004|Driver's **SQLAllocHandle** on SQL_HANDLE_ENV failed|(DM) During **SQLDriverConnect**, the Driver Manager called the driver's **SQLAllocHandle** function with an *fHandleType* of SQL_HANDLE_ENV and the driver returned an error.|  
|IM005|Driver's **SQLAllocHandle** on SQL_HANDLE_DBC failed.|(DM) During **SQLDriverConnect**, the Driver Manager called the driver's **SQLAllocHandle** function with an *fHandleType* of SQL_HANDLE_DBC and the driver returned an error.|  
|IM006|Driver's **SQLSetConnectAttr** failed|(DM) During **SQLDriverConnect**, the Driver Manager called the driver's **SQLSetConnectAttr** function and the driver returned an error.|  
|IM007|No data source or driver specified; dialog prohibited|No data source name or driver was specified in the connection string, and *DriverCompletion* was SQL_DRIVER_NOPROMPT.|  
|IM008|Dialog failed|The driver attempted to display its login dialog box and failed.<br /><br /> *WindowHandle* was a null pointer, and *DriverCompletion* was not SQL_DRIVER_NO_PROMPT.|  
|IM009|Unable to load translation DLL|The driver was unable to load the translation DLL that was specified for the data source or for the connection.|  
|IM010|Data source name too long|(DM) The attribute value for the DSN keyword was longer than SQL_MAX_DSN_LENGTH characters.|  
|IM011|Driver name too long|(DM) The attribute value for the **DRIVER** keyword was longer than 255 characters.|  
|IM012|DRIVER keyword syntax error|(DM) The keyword-value pair for the **DRIVER** keyword contained a syntax error.<br /><br /> (DM) The string in *\*InConnectionString* contained a **FILEDSN** keyword, but the .dsn file did not contain a **DRIVER** keyword or a **DSN** keyword.|  
|IM014|The specified DSN contains an architecture mismatch between the Driver and Application|(DM) 32-bit application uses a DSN connecting to a 64-bit driver; or vice versa.|  
|IM015|Driver's SQLDriverConnect on SQL_HANDLE_DBC_INFO_HANDLE failed|If a driver returns SQL_ERROR, the Driver Manager will return SQL_ERROR to the application and the connection will fail.<br /><br /> For more information about SQL_HANDLE_DBC_INFO_TOKEN, see [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md).|  
|IM017|Polling is disabled in asynchronous notification mode|Whenever the notification model is used, polling is disabled.|  
|IM018|**SQLCompleteAsync** has not been called to complete the previous asynchronous operation on this handle.|If the previous function call on the handle returns SQL_STILL_EXECUTING and if notification mode is enabled, **SQLCompleteAsync** must be called on the handle to do post-processing and complete the operation.|  
|S1118|Driver does not support asynchronous notification|When the driver does not support asynchronous notification, you cannot set SQL_ATTR_ASYNC_DBC_EVENT or SQL_ATTR_ASYNC_DBC_RETCODE_PTR.|  
  
## Comments  
 A connection string has the following syntax:  
  
 *connection-string* ::= *empty-string*[;] &#124; *attribute*[;] &#124; *attribute*; *connection-string*  
  
 *empty-string* ::=*attribute* ::= *attribute-keyword*=*attribute-value* &#124; DRIVER=[{]*attribute-value*[}]  
  
 *attribute-keyword* ::= DSN &#124; UID &#124; PWD &#124; *driver-defined-attribute-keyword*  
  
 *attribute-value* ::= *character-string*  
  
 *driver-defined-attribute-keyword* ::= *identifier*  
  
 where *character-string* has zero or more characters; *identifier* has one or more characters; *attribute-keyword* is not case-sensitive; *attribute-value* may be case-sensitive; and the value of the **DSN** keyword does not consist solely of blanks.  
  
 Because of connection string and initialization file grammar, keywords and attribute values that contain the characters **[]{}(),;?\*=!@** not enclosed with braces should be avoided. The value of the **DSN** keyword cannot consist only of blanks and should not contain leading blanks. Because of the grammar of the system information, keywords and data source names cannot contain the backslash (\\) character.  
  
 Applications do not have to add braces around the attribute value after the **DRIVER** keyword unless the attribute contains a semicolon (;), in which case the braces are required. If the attribute value that the driver receives includes braces, the driver should not remove them but they should be part of the returned connection string.  
  
 A DSN or connection string value enclosed with braces ({}) containing any of the characters **[]{}(),;?\*=!@** is passed intact to the driver. However, when using these characters in a keyword, the Driver Manager returns an error when working with file DSNs but passes the connection string to the driver for regular connection strings. Avoid using embedded braces in a keyword value.  
  
 The connection string may include any number of driver-defined keywords. Because the **DRIVER** keyword does not use information from the system information, the driver must define enough keywords so that a driver can connect to a data source using only the information in the connection string. (For more information, see "Driver Guidelines," later in this section.) The driver defines which keywords are required to connect to the data source.  
  
 The following table describes the attribute values of the **DSN**, **FILEDSN**, **DRIVER**, **UID**, **PWD**, and **SAVEFILE** keywords.  
  
|Keyword|Attribute value description|  
|-------------|---------------------------------|  
|**DSN**|Name of a data source as returned by **SQLDataSources** or the data sources dialog box of **SQLDriverConnect**.|  
|**FILEDSN**|Name of a .dsn file from which a connection string will be built for the data source. These data sources are called file data sources.|  
|**DRIVER**|Description of the driver as returned by the **SQLDrivers** function. For example, Rdb or SQL Server.|  
|**UID**|A user ID.|  
|**PWD**|The password corresponding to the user ID, or an empty string if there is no password for the user ID (PWD=;).|  
|**SAVEFILE**|The file name of a .dsn file in which the attribute values of keywords used in making the present, successful connection should be saved.|  
  
 For information about how an application chooses a data source or driver, see [Choosing a Data Source or Driver](../../../odbc/reference/develop-app/choosing-a-data-source-or-driver.md).  
  
 If any keywords are repeated in the connection string, the driver uses the value associated with the first occurrence of the keyword. If the **DSN** and **DRIVER** keywords are included in the same connection string, the Driver Manager and the driver use whichever keyword appears first.  
  
 The **FILEDSN** and **DSN** keywords are mutually exclusive: whichever keyword appears first is used, and the one that appears second is ignored. The **FILEDSN** and **DRIVER** keywords, on the other hand, are not mutually exclusive. If any keyword appears in a connection string with **FILEDSN**, then the attribute value of the keyword in the connection string is used rather than the attribute value of the same keyword in the .dsn file.  
  
 If the **FILEDSN** keyword is used, the keywords specified in a .dsn file are used to create a connection string. (For more information, see "File Data Sources," later in this section.) The **UID** keyword is optional; a .dsn file may be created with only the **DRIVER** keyword. The **PWD** keyword is not stored in a .dsn file. The default directory for saving and loading a .dsn file will be a combination of the path specified by **CommonFileDir** in HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\ Windows\CurrentVersion and "ODBC\DataSources". (If CommonFileDir were "C:\Program Files\Common Files", the default directory would be "C:\Program Files\Common Files\ODBC\Data Sources".)  
  
> [!NOTE]  
>  A .dsn file can be manipulated directly by calling the [SQLReadFileDSN](../../../odbc/reference/syntax/sqlreadfiledsn-function.md) and [SQLWriteFileDSN](../../../odbc/reference/syntax/sqlwritefiledsn-function.md) functions in the installer DLL.  
  
 If the **SAVEFILE** keyword is used, the attribute values of keywords used in making the present, successful connection will be saved as a .dsn file with the name of the attribute value of the **SAVEFILE** keyword. The **SAVEFILE** keyword must be used in conjunction with the **DRIVER** keyword, the **FILEDSN** keyword, or both, or the function returns SQL_SUCCESS_WITH_INFO with SQLSTATE 01S09 (Invalid keyword). The **SAVEFILE** keyword must appear before the **DRIVER** keyword in the connection string, or the results will be undefined.  
  
## Driver Manager Guidelines  
 The Driver Manager constructs a connection string to pass to the driver in the *InConnectionString* argument of the driver's **SQLDriverConnect** function. The Driver Manager does not modify the *InConnectionString* argument passed to it by the application.  
  
 The action of the Driver Manager is based on the value of the *DriverCompletion* argument:  
  
-   SQL_DRIVER_PROMPT: If the connection string does not contain either the **DRIVER**, **DSN**, or **FILEDSN** keyword, the Driver Manager displays the Data Sources dialog box. It constructs a connection string from the data source name returned by the dialog box and any other keywords passed to it by the application. If the data source name returned by the dialog box is empty, the Driver Manager specifies the keyword-value pair DSN=Default. (This dialog box will not display a data source with the name "Default".)  
  
-   SQL_DRIVER_COMPLETE or SQL_DRIVER_COMPLETE_REQUIRED: If the connection string specified by the application includes the **DSN** keyword, the Driver Manager copies the connection string specified by the application. Otherwise, it takes the same actions as it does when *DriverCompletion* is SQL_DRIVER_PROMPT.  
  
-   SQL_DRIVER_NOPROMPT: The Driver Manager copies the connection string specified by the application.  
  
 If the connection string specified by the application contains the **DRIVER** keyword, the Driver Manager copies the connection string specified by the application.  
  
 Using the connection string it has constructed, the Driver Manager determines which driver to use, connects to that driver, and passes the connection string it has constructed to the driver; for more information about the interaction of the Driver Manager and the driver, see the "Comments" section in [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md). If the connection string does not contain the **DRIVER** keyword, the Driver Manager determines which driver to use as follows:  
  
1.  If the connection string contains the **DSN** keyword, the Driver Manager retrieves the driver associated with the data source from the system information.  
  
2.  If the connection string does not contain the **DSN** keyword or the data source is not found, the Driver Manager retrieves the driver associated with the Default data source from the system information. (For more information, see [Default Subkey](../../../odbc/reference/install/default-subkey.md).) The Driver Manager changes the value of the **DSN** keyword in the connection string to "DEFAULT".  
  
3.  If the **DSN** keyword in the connection string is set to "DEFAULT", the Driver Manager retrieves the driver associated with the Default data source from the system information.  
  
4.  If the data source is not found and the Default data source is not found, the Driver Manager returns SQL_ERROR with SQLSTATE IM002 (Data source not found and no default driver specified).  
  
## File Data Sources  
 If the connection string specified by the application in the call to **SQLDriverConnect** contains the **FILEDSN** keyword, and this keyword is not superseded by either the **DSN** or **DRIVER** keyword, then the Driver Manager creates a connection string using the information in the .dsn file and the *InConnectionString* argument. The Driver Manager proceeds as follows:  
  
1.  Checks whether the file name of the .dsn file is valid. If not, it returns SQL_ERROR with SQLSTATE IM014 (Invalid name of file DSN). If the file name is an empty string ("") and SQL_DRIVER_NOPROMPT is not specified, then the **File-Open** dialog box is displayed. If the file name contains a valid path but no file name or an invalid file name, and SQL_DRIVER_NOPROMPT is not specified, then the **File-Open** dialog box is displayed with the current directory set to the one specified in the file name. If the file name is an empty string ("") or the file name contains a valid path but no file name or an invalid file name, and SQL_DRIVER_NOPROMPT is specified, then SQL_ERROR is returned with SQLSTATE IM014 (Invalid name of file DSN).  
  
2.  Reads all keywords in the [ODBC] section of the .dsn file. If the **DRIVER** keyword is not present, it returns SQL_ERROR with SQLSTATE IM012 (Driver keyword syntax error), except where the .dsn file is unshareable and thus contains only the **DSN** keyword.  
  
     If the file data source is unshareable, the Driver Manager reads the value of the **DSN** keyword and connects as necessary to the user or system data source pointed to by the unshareable file data source. Steps 3 through 5 are not performed.  
  
3.  Constructs a connection string for the driver. The driver connection string is the union of the keywords specified in the .dsn file and those specified in the original application connection string. Rules for the construction of the driver connection string where keywords overlap are as follows:  
  
    -   If the **DRIVER** keyword exists in the application connection string and the drivers specified by the **DRIVER** keywords are not the same in the .dsn file and the application connection string, then the driver information in the .dsn file is ignored and the driver information in the application connection string is used. If the drivers specified by the **DRIVER** keyword are the same in the .dsn file and the application's connection string, then where all keywords overlap, those specified in the application connection string have precedence over those specified in the .dsn file.  
  
    -   In the new connection string, the **FILEDSN** keyword is eliminated.  
  
4.  Loads the driver by looking in the registry entry HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI\\<Driver Name\>\Driver where \<Driver Name> is specified by the **DRIVER** keyword.  
  
5.  Passes the driver the new connections string.  
  
 For examples of .dsn files, see [Connecting Using File Data Sources](../../../odbc/reference/develop-app/connecting-using-file-data-sources.md).  
  
## SAVEFILE Keyword  
 If the connection string specified by the application contains the **SAVEFILE** keyword, then the Driver Manager saves the connection string in a .dsn file. The Driver Manager proceeds as follows:  
  
1.  Checks whether the file name of the .dsn file included as the attribute value of the **SAVEFILE** keyword is valid. If not, it returns SQL_ERROR with SQLSTATE IM014 (Invalid name of file DSN). The validity of the file name is determined by standard system naming rules. If the file name is an empty string ("") and the *DriverCompletion* argument is not SQL_DRIVER_NOPROMPT, then the file name is valid. If the file name already exists, then if *DriverCompletion* is SQL_DRIVER_NOPROMPT, the file is overwritten. If *DriverCompletion* is SQL_DRIVER_PROMPT, SQL_DRIVER_COMPLETE, or SQL_DRIVER_COMPLETE_REQUIRED, a dialog box prompts the user to specify whether the file should be overwritten. If No is entered, then the **File-Save** dialog box appears.  
  
2.  If the driver returns SQL_SUCCESS and the file name was not an empty string, then the Driver Manager writes the connection information returned in the *OutConnectionString* argument to the specified file with the format specified in the "Connection Strings" section earlier in this section.  
  
3.  If the driver returns SQL_SUCCESS and the file name was an empty string (""), then the Driver Manager calls the **File-Save** common dialog box with the *hwnd* specified and writes the connection information returned in *OutConnectionString* to the file specified in the File-Save common dialog box with the format specified in the "Connection Strings" section earlier in this section.  
  
4.  If the driver returns SQL_SUCCESS, it returns the *OutConnectionString* argument containing the connection string to the application.  
  
5.  If the driver returns SQL_SUCCESS_WITH_INFO or SQL_ERROR, then the Driver Manager returns the SQLSTATE to the application.  
  
## Driver Guidelines  
 The driver checks whether the connection string passed to it by the Driver Manager contains the **DSN** or **DRIVER** keyword. If the connection string contains the **DRIVER** keyword, the driver cannot retrieve information about the data source from the system information. If the connection string contains the **DSN** keyword or does not contain either the **DSN** or the **DRIVER** keyword, the driver can retrieve information about the data source from the system information as follows:  
  
1.  If the connection string contains the **DSN** keyword, the driver retrieves the information for the specified data source.  
  
2.  If the connection string does not contain the **DSN** keyword, the specified data source is not found, or the **DSN** keyword is set to "DEFAULT", the driver retrieves the information for the Default data source.  
  
 The driver uses any information it retrieves from the system information to augment the information passed to it in the connection string. If the information in the system information duplicates information in the connection string, the driver uses the information in the connection string.  
  
 Based on the value of *DriverCompletion*, the driver prompts the user for connection information, such as the user ID and password, and connects to the data source:  
  
-   SQL_DRIVER_PROMPT: The driver displays a dialog box, using the values from the connection string and system information (if any) as initial values. When the user exits the dialog box, the driver connects to the data source. It also constructs a connection string from the value of the **DSN** or **DRIVER** keyword in \**InConnectionString* and the information returned from the dialog box. It places this connection string in the **OutConnectionString* buffer.  
  
-   SQL_DRIVER_COMPLETE or SQL_DRIVER_COMPLETE_REQUIRED: If the connection string contains enough information, and that information is correct, the driver connects to the data source and copies \**InConnectionString* to \**OutConnectionString*. If any information is missing or incorrect, the driver takes the same actions as it does when *DriverCompletion* is SQL_DRIVER_PROMPT, except that if *DriverCompletion* is SQL_DRIVER_COMPLETE_REQUIRED, the driver disables the controls for any information not required to connect to the data source.  
  
-   SQL_DRIVER_NOPROMPT: If the connection string contains enough information, the driver connects to the data source and copies \**InConnectionString* to \**OutConnectionString*. Otherwise, the driver returns SQL_ERROR for **SQLDriverConnect**.  
  
 On successful connection to the data source, the driver also sets \**StringLength2Ptr* to the length of the output connection string that is available to return in **OutConnectionString*.  
  
 If the user cancels a dialog box presented by the Driver Manager or the driver, **SQLDriverConnect** returns SQL_NO_DATA.  
  
 For information about how the Driver Manager and the driver interact during the connection process, see [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
 If a driver supports **SQLDriverConnect**, the driver keyword section of the system information for the driver must contain the **ConnectFunctions** keyword with the second character set to "Y".  
  
## Connecting When Connection Pooling Is Enabled  
 Connection pooling allows an application to reuse a connection that has already been created. When **SQLDriverConnect** is called, the Driver Manager attempts to make the connection using a connection that is part of a pool of connections in an environment that has been designated for connection pooling. For more information on connection pooling, see [SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md).  
  
 An application can set SQL_ATTR_RESET_CONNECTION before calling SQLDisconnect on a connection where pooling is enabled. For more information, see [SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md).  
  
 The following restrictions apply when an application calls **SQLDriverConnect** to connect to a pooled connection:  
  
-   No connection pooling processing is performed when the **SAVEFILE** keyword is specified in the connection string.  
  
-   If connection pooling is enabled, **SQLDriverConnect** can be called only with a *DriverCompletion* argument of SQL_DRIVER_NOPROMPT; if **SQLDriverConnect** is called with any other *DriverCompletion*, SQLSTATE HY110 (Invalid driver completion) will be returned.  
  
## Connection Attributes  
 The SQL_ATTR_LOGIN_TIMEOUT connection attribute, set using **SQLSetConnectAttr**, defines the number of seconds to wait for a login request to complete with a successful connection by the driver before returning to the application. If the user is prompted to complete the connection string, a waiting period for each login request begins when the driver starts the connection process.  
  
 The driver opens the connection in SQL_MODE_READ_WRITE access mode by default. To set the access mode to SQL_MODE_READ_ONLY, the application must call **SQLSetConnectAttr** with the SQL_ATTR_ACCESS_MODE attribute prior to calling **SQLDriverConnect**.  
  
 If a default translation library is specified in the system information for the data source, the driver loads it. A different translation library can be loaded by calling **SQLSetConnectAttr** with the SQL_ATTR_TRANSLATE_LIB attribute. A translation option can be specified by calling **SQLSetConnectAttr** with the SQL_ATTR_TRANSLATE_OPTION option.  
  
 For more information, see [Connecting with SQLDriverConnect](../../../odbc/reference/develop-app/connecting-with-sqldriverconnect.md).  
  
```cpp  
// SQLDriverConnect_ref.cpp  
// compile with: odbc32.lib user32.lib  
#include <windows.h>  
#include <sqlext.h>  
  
int main() {  
   SQLHENV henv;  
   SQLHDBC hdbc;  
   SQLHSTMT hstmt;  
   SQLRETURN retcode;  
  
   SQLCHAR OutConnStr[255];  
   SQLSMALLINT OutConnStrLen;  
  
   HWND desktopHandle = GetDesktopWindow();   // desktop's window handle  
  
   // Allocate environment handle  
   retcode = SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv);  
  
   // Set the ODBC version environment attribute  
   if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
      retcode = SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION, (SQLPOINTER*)SQL_OV_ODBC3, 0);   
  
      // Allocate connection handle  
      if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
         retcode = SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc);   
  
         // Set login timeout to 5 seconds  
         if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
            SQLSetConnectAttr(hdbc, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0);  
  
            retcode = SQLDriverConnect( // SQL_NULL_HDBC  
               hdbc,   
               desktopHandle,   
               (SQLCHAR*)"driver=SQL Server",   
               _countof("driver=SQL Server"),  
               OutConnStr,  
               255,   
               &OutConnStrLen,  
               SQL_DRIVER_PROMPT );  
  
            // Allocate statement handle  
            if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {                 
               retcode = SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt);   
  
               // Process data  
               if (retcode == SQL_SUCCESS || retcode == SQL_SUCCESS_WITH_INFO) {  
                  SQLFreeHandle(SQL_HANDLE_STMT, hstmt);  
               }  
  
               SQLDisconnect(hdbc);  
            }  
  
            SQLFreeHandle(SQL_HANDLE_DBC, hdbc);  
         }  
      }  
      SQLFreeHandle(SQL_HANDLE_ENV, henv);  
   }  
}  
```  
  
 Also see [Sample ODBC Program](../../../odbc/reference/sample-odbc-program.md).  
  
## Related Functions  
  
|For information about|See|  
|---------------------------|---------|  
|Allocating a handle|[SQLAllocHandle Function](../../../odbc/reference/syntax/sqlallochandle-function.md)|  
|Discovering and enumerating values required to connect to a data source|[SQLBrowseConnect Function](../../../odbc/reference/syntax/sqlbrowseconnect-function.md)|  
|Connecting to a data source|[SQLConnect Function](../../../odbc/reference/syntax/sqlconnect-function.md)|  
|Disconnecting from a data source|[SQLDisconnect Function](../../../odbc/reference/syntax/sqldisconnect-function.md)|  
|Returning driver descriptions and attributes|[SQLDrivers Function](../../../odbc/reference/syntax/sqldrivers-function.md)|  
|Freeing a handle|[SQLFreeHandle Function](../../../odbc/reference/syntax/sqlfreehandle-function.md)|  
|Setting a connection attribute|[SQLSetConnectAttr Function](../../../odbc/reference/syntax/sqlsetconnectattr-function.md)|  
  
## See Also  
 [ODBC API Reference](../../../odbc/reference/syntax/odbc-api-reference.md)   
 [ODBC Header Files](../../../odbc/reference/install/odbc-header-files.md)
