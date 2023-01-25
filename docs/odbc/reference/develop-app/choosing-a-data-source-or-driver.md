---
description: "Choosing a Data Source or Driver"
title: "Choosing a Data Source or Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connecting to driver [ODBC], selecting driver"
  - "connecting to data source [ODBC], selecting data source"
  - "data sources [ODBC], selecting"
  - "ODBC drivers [ODBC], selecting"
ms.assetid: 10aaf570-01ab-4478-8339-bdde2a5e3dd1
author: David-Engel
ms.author: v-davidengel
---
# Choosing a Data Source or Driver
The data source or driver used by an application is sometimes hard-coded in the application. For example, a custom application written by an MIS department to transfer data from one data source to another would contain the names of those data sources-the application simply would not work with any other data sources. Another example is a vertical application, such as one used for order entry. Such an application always uses the same data source, which has a predefined schema known by the application.  
  
 Other applications select the data source or driver at run time. Usually, these are generic applications that do ad hoc queries, such as a spreadsheet that uses ODBC to import data. Such applications usually list the available data sources or drivers and let users choose the ones they want to work with. Whether a generic application lists data sources, drivers, or both frequently depends on whether the application uses DBMS-based or file-based drivers.  
  
 DBMS-based drivers usually require a complex set of connection information, such as the network address, network protocol, database name, and so on. The purpose of a data source is to hide all of this information. Therefore, the data source paradigm lends itself to use with DBMS-based drivers. An application can display a list of data sources to the user in one of two ways. It can call **SQLDriverConnect** with the **DSN** (Data Source Name) keyword and no associated value; the Driver Manager will display a list of data source names. If the application wants control over the appearance of the list, it calls **SQLDataSources** to retrieve a list of available data sources and constructs its own dialog box. This function is implemented by the Driver Manager and can be called before any drivers are loaded. The application then calls a connection function and passes it the name of the chosen data source.  
  
 If a data source is not specified, the default data source indicated by the system information is used. (For more information, see [Default Subkey](../../../odbc/reference/install/default-subkey.md).) If **SQLConnect** is called by using a *ServerName* argument that cannot be found, is a null pointer, or is "DEFAULT", the Driver Manager connects to the default data source. The default data source is also used if the connection string that is used in a call to **SQLDriverConnect** or **SQLBrowseConnect** contains the **DSN** keyword set to "DEFAULT" or if the specified data source is not found. Additionally, the default data source is used if the connection string that is used in a call to **SQLDriverConnect** does not contain the **DSN** keyword.  
  
 With file-based drivers, it is possible to use a file paradigm. For data stored on the local computer, users frequently know that their data is in a particular file, such as Employee.dbf. Instead of selecting an unknown data source, it is easier for such users to select the file they know. To implement this, the application first calls **SQLDrivers**. This function is implemented by the Driver Manager and can be called before any drivers are loaded. **SQLDrivers** returns a list of available drivers; it also returns values for the **FileUsage** and **FileExtns** keywords. The **FileUsage** keyword explains whether file-based drivers treat files as tables, as does Xbase, or as databases, as does Microsoft® Access. The **FileExtns** keyword lists the file name extensions the driver recognizes, such as .dbf for an Xbase driver. Using this information, the application constructs a dialog box through which the user chooses a file. Based on the extension of the chosen file, the application then connects to the driver by calling **SQLDriverConnect** with the **DRIVER** keyword.  
  
 There is nothing to stop an application from using a data source with a file-based driver or calling **SQLDriverConnect** with the **DRIVER** keyword to connect to a DBMS-based driver. Here are several common uses of the **DRIVER** keyword for DBMS-based drivers:  
  
-   **Not creating data sources.** For example, a custom application might use a particular driver and database. If the driver name and all information that is required to connect to the database is hard-coded in the application, users do not have to create a data source on their computer to run the application. All they must do is install the application and driver.  
  
     A disadvantage of this method is that the application must be recompiled and redistributed if the connection information changes. If a data source name is hard-coded in the application instead of complete connection information, each user must change only the information in the data source.  
  
-   **Accessing a particular DBMS a single time.** For example, a spreadsheet that retrieves data by calling ODBC functions might contain the **DRIVER** keyword to identify a particular driver. Because the driver name is meaningful to any users who have that driver, the spreadsheet could be passed among those users. If the spreadsheet contained a data source name, each user would have to create the same data source to use the spreadsheet.  
  
-   **Browsing the system for all databases accessible to a particular driver.** For more information, see [Connecting with SQLBrowseConnect](../../../odbc/reference/develop-app/connecting-with-sqlbrowseconnect.md), later in this section.
