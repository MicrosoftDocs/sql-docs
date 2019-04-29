---
title: "Preparing to Implement a Data Processing Extension | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "interfaces [Reporting Services]"
  - "data processing extensions [Reporting Services], implementing"
ms.assetid: 698817e4-33da-4eb5-9407-4103e1c35247
author: maggiesMSFT
ms.author: maggies
---
# Preparing to Implement a Data Processing Extension
  Before you implement your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension, you should define the interfaces to implement. You may want to provide extension-specific implementations of the entire set of interfaces, or you may simply want to focus your implementation on a subset, such as the <xref:Microsoft.ReportingServices.DataProcessing.IDataReader> and <xref:Microsoft.ReportingServices.DataProcessing.IDbCommand> interfaces in which clients would interact primarily with a result set as a **DataReader** object and would use your [!INCLUDE[ssRS](../../../includes/ssrs.md)] data processing extension as a bridge between the result set and your data source.  
  
 You can implement data processing extensions in one of two ways:  
  
-   Your data processing extension classes can implement the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] data provider interfaces and optionally the extended data processing extension interfaces provided by [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
-   Your data processing extension classes can implement the data processing extension interfaces provided by [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and optionally the extended data processing extension interfaces.  
  
 If your [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension will not support a particular property or method, implement the property or method as no-operation. If a client expects a particular behavior, throw a **NotSupportedException** exception.  
  
> [!NOTE]  
>  A no-operation implementation of a property or method only applies to the properties and methods of those interfaces that you choose to implement. Optional interfaces that you choose not to implement should be left out of your data processing extension assembly. For more information about whether an interface is required or optional, see the table later in this section.  
  
## Required Extension Functionality  
 Each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension must provide the following functionality:  
  
-   Open a connection to a data source.  
  
-   Analyze a query and return a list of field names for the result set.  
  
-   Execute a query against the data source and return a row set.  
  
-   Pass single-valued parameters to the query.  
  
-   Iterate through rows in the row set and retrieve data.  
  
 Each data processing extension can be extended to include the following functionality:  
  
-   Analyze a query and return a list of parameter names used in the query.  
  
-   Analyze a query and return the list of fields by which the query is grouped.  
  
-   Analyze a query and return the list of fields by which the query is sorted.  
  
-   Provide a user name and password to connect to the data source that is independent of the connection string.  
  
-   Iterate through rows in the row set and retrieve auxiliary metadata about the data values.  
  
-   Aggregate data at the server.  
  
## Available Extension Interfaces  
 The following table describes the available interfaces and whether implementation is required or optional.  
  
|Interface|Description|Implementation|  
|---------------|-----------------|--------------------|  
|IDbConnection|Represents a unique session with a data source. In the case of a client/server database system, the session may be equivalent to a network connection to the server.|Required|  
|IDbConnectionExtension|Represents additional connection properties that can be implemented by [!INCLUDE[ssRS](../../../includes/ssrs.md)] data processing extensions regarding security and authentication.|Optional|  
|IDbTransaction|Represents a local transaction.|Required|  
|IDbTransactionExtension|Represents additional transaction properties that can be implemented by [!INCLUDE[ssRS](../../../includes/ssrs.md)] data processing extensions.|Optional|  
|IDbCommand|Represents a query or command that is used when connected to a data source.|Required|  
|IDbCommandAnalysis|Represents additional command information for analyzing a query and returning a list of parameter names used in the query.|Optional|  
|IDataParameter|Represents a parameter or name/value pair that is passed to a command or query.|Required|  
|IDataParameterCollection|Represents a collection of all parameters relevant to a command or query.|Required|  
|IDataReader|Provides a method of reading a forward-only, read-only stream of data from your data source.|Required|  
|IDataReaderExtension|Provides a method of reading one or more forward-only streams of result sets, obtained by executing a command at a data source. This interface provides additional support for field aggregates.|Optional|  
|IExtension|Provides the base class for a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension. Also enables an implementer to include a localized name for the extension and to pass configuration settings from the configuration file to the extension.|Required|  
  
 The data processing extension interfaces are identical to a subset of the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] data provider interfaces, methods, and properties whenever possible. For more information about implementing a full [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] data provider, see "Implementing a .NET Framework Data Provider" in your [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] Software Development Kit (SDK) documentation.  
  
## See Also  
 [Reporting Services Extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
 [Implementing a Data Processing Extension](../../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
