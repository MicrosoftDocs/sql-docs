---
title: "Find Property Set GUIDs and Property Integer IDs for Search Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], search property lists"
  - "search property lists [SQL Server], configuring"
ms.assetid: 7db79165-8bcc-4be6-8d40-12d44deda79f
author: douglaslMS
ms.author: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Find Property Set GUIDs and Property Integer IDs for Search Properties
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic discusses how to obtain the values that are required before you can add a property to a search property list and make it searchable by full-text search. These values include the property set GUID and property integer identifier of a document property.  
  
 Document properties that are extracted by IFilters from binary data - that is, from data stored in a **varbinary**, **varbinary(max)** (including **FILESTREAM**), or **image** data type column - can be made available for full-text search. To make an extracted property searchable, the property must be manually added to a search property list. The search property list must also be associated with one or more full-text indexes. For more information, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
 Before you can add an available property to a property list, you have to find 2 pieces of information about the property:  
  
-   The property set GUID of the property.  
  
-   The integer ID of the property.  
  
 (When you add a property to a property list, you also have to provide a name and description. However you do not have to use the canonical name and description of the property.)  
  
 This topic describes the commonly-used methods to find information about available properties, especially about properties that are defined by Microsoft. For information about properties that have been defined by a third party, refer to the third-party documentation or contact the vendor.  
  
##  <a name="wellknown"></a> Finding Information about Widely Used, Well-Known Microsoft Properties  
 Microsoft defines hundreds of document properties for use in many contexts, but only a small subset of the available properties are used by each file format. Among the frequently used Windows properties is a small set of generic properties. Some examples of well-known generic properties are shown in the following table. The table shows the well-known name, the Windows canonical name (from the property description published by Microsoft), the property set GUID, the property integer identifier, and a brief description.  
  
|Well-known name|Windows canonical name|Property set GUID|Integer ID|Description|  
|----------------------|----------------------------|-----------------------|----------------|-----------------|  
|Authors|**System.Author**|F29F85E0-4FF9-1068-AB91-08002B27B3D9|4|Author or authors of a given item.|  
|Tags|**System.Keywords**|F29F85E0-4FF9-1068-AB91-08002B27B3D9|5|Set of keywords (also known as tags) assigned to the item.|  
|Type|**System.PerceivedType**|28636AA6-953D-11D2-B5D6-00C04FD918D0|9|Perceived file type based on its canonical type.|  
|Title|**System.Title**|F29F85E0-4FF9-1068-AB91-08002B27B3D9|2|Title of the item. For example, the title of a document, the subject of a message, the caption of a photo, or the name of a music track.|  
  
 To encourage consistency among file formats, Microsoft has identified subsets of frequently used, high-priority document properties for several categories of documents. These include communications, contacts, documents, music files, pictures, and videos. For more information about the top-ranked properties for each category, see [system-defined properties for custom file formats](https://go.microsoft.com/fwlink/?LinkId=144336) in the Windows Search documentation.  
  
 A specific file format might implement properties of three types:  
  
-   Generic properties defined by [!INCLUDE[msCoName](../../includes/msconame-md.md)].  
  
-   Category-specific properties defined by [!INCLUDE[msCoName](../../includes/msconame-md.md)].  
  
-   Custom, application-specific properties defined by the software vendor.  
  
##  <a name="filtdump"></a> Finding Information about Available Properties by using FILTDUMP.EXE  
 To learn what properties are discovered and extracted by an installed IFilter, you can install and run the **filtdump.exe** utility, which is part of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows SDK.  
  
 You run **filtdump.exe** from the command prompt and provide a single argument. This argument is the name of an individual file that has a file type for which an IFilter is installed. The utility displays a list of all the properties discovered by the IFilter in the document, with their property set GUIDs, integer IDs, and additional information.  
  
 For information about installing this software, see [Microsoft Windows SDK for Windows 7 and .NET Framework 4](https://go.microsoft.com/fwlink/?LinkId=212980). After you download and install the SDK, look in the following folders for the filtdump.exe utility.  
  
-   For the 64-bit version, look in `C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\x64`.  
  
-   For the 32-bit version, look in `C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin`.  
  
##  <a name="propdesc"></a> Finding Values for a Search Property from a Windows Property Description  
 For a well-known Windows search property, you can obtain the information that you need from the **formatID** and **propID** attributes of the property description (**propertyDescription**).  
  
 The following example shows the relevant part of a typical Microsoft property description, in this case, of the `System.Author` property. The `formatID` attribute specifies the property set GUID, `F29F85E0-4FF9-1068-AB91-08002B27B3D9`, and the `propID` attribute specifies the property integer ID, `4.` Notice that the `name` attribute specifies the Windows canonical property name, `System.Author`. (This example omits portions of the property description that are not relevant.)  
  
```  
.  
propertyDescription  
name = System.Author  
...  
formatID = F29F85E0-4FF9-1068-AB91-08002B27B3D9  
propID = 4  
...  
```  
  
 For the complete description of this property, see [System.Author](https://go.microsoft.com/fwlink/?LinkId=144337) in the Windows Search documentation.  
  
 For a complete list of Windows properties, see [Windows Properties](https://go.microsoft.com/fwlink/?LinkId=215013), also in the Windows Search documentation.  
  
##  <a name="examples"></a> Adding a Property to a Search Property List  
 The following example shows how to add a property to a search property list. The example uses an [ALTER SEARCH PROPERTY LIST](../../t-sql/statements/alter-search-property-list-transact-sql.md) statement to add the `System.Author` property to a search property list named `PropertyList1`, and provides a user friendly name for the property, `Author`.  
  
```  
ALTER SEARCH PROPERTY LIST PropertyList1   
  ADD 'Author'  
    WITH (  
          PROPERTY_SET_GUID = 'F29F85E0-4FF9-1068-AB91-08002B27B3D9',  
          PROPERTY_INT_ID = 4,   
          PROPERTY_DESCRIPTION = 'System.Author - the author or authors of the item'   
         )  
GO  
```  
  
 For more information about creating a search property list and associating it with a full-text index, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
## See Also  
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)   
 [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md)  
  
  
