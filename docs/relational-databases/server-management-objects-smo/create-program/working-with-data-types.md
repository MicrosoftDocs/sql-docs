---
description: "Working with Data Types"
title: "Working with Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "DataType object"
  - "SQL Server Management Objects, data types"
  - "data types [SMO]"
  - "SMO [SQL Server], data types"
ms.assetid: 1e0e736a-c709-4d89-aeb2-b32dcfd641fa
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Working with Data Types
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Data comes in many types and sizes, such as a string that has a defined length, a number that has specific accuracy, or a user-defined data type that is another object that has its own set of rules. The <xref:Microsoft.SqlServer.Management.Smo.DataType> object classifies the type of data so that it can be handled correctly by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The <xref:Microsoft.SqlServer.Management.Smo.DataType> object is associated with objects that accept data. The following [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO) objects accept data that must be defined by a <xref:Microsoft.SqlServer.Management.Smo.DataType> object property:  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Column>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.UserDefinedDataType>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.UserDefinedType>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.UserDefinedFunctionParameter>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.StoredProcedureParameter>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.UserDefinedFunctionParameter>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.UserDefinedAggregateParameter>  
  
 The **DataType** property for objects that accept data can be set in several ways.  
  
-   Use the default constructor and specify <xref:Microsoft.SqlServer.Management.Smo.DataType> object properties explicitly  
  
-   Use an overloaded constructor and specify the <xref:Microsoft.SqlServer.Management.Smo.DataType> properties as parameters.  
  
-   Specify the <xref:Microsoft.SqlServer.Management.Smo.DataType> inline in the object constructor.  
  
-   Use one of the static members of the <xref:Microsoft.SqlServer.Management.Smo.DataType> class, for example **Int**. This will in fact return an instance of a <xref:Microsoft.SqlServer.Management.Smo.DataType> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.DataType> object has several properties that define the type of data. For example, the <xref:Microsoft.SqlServer.Management.Smo.SqlDataType> property specifies the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type. The constant values that represent [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types are listed in the <xref:Microsoft.SqlServer.Management.Smo.SqlDataType> enumeration. This refers to data types such as **varchar**, **nchar**, **currency**, **integer**, **float**, and **datetime**.  
  
 When the data type is established, specific properties must be set for the data. For example, if it is an **nchar** type, the length of the string data must be set in the **Length** property. The same applies for numeric values, where you would have to specify precision and scale.  
  
 <xref:Microsoft.SqlServer.Management.Smo.UserDefinedDataType> and <xref:Microsoft.SqlServer.Management.Smo.UserDefinedType> data types refer to objects that contain the definition of the type of data defined by the user. The <xref:Microsoft.SqlServer.Management.Smo.UserDefinedDataType> is based on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types from the <xref:Microsoft.SqlServer.Management.Smo.SqlDataType> enumeration. The <xref:Microsoft.SqlServer.Management.Smo.UserDefinedType> is based on [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET data types. Typically, these would represent data of a specific type that is frequently reused by the database because of business rules defined by the organization. For example, a data type that stores an amount of money and a currency denominator would be helpful in a company that deals in multiple currencies.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.SqlDataType> enumeration contains a list of all the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-supported data types.  
  
## Examples  
To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
  
## Constructing a DataType Object with the Specification in the Constructor in Visual Basic  
 This code example shows how to use the constructor to create instances of data types that are based on different [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.  
  
> [!NOTE]  
>  The <xref:Microsoft.SqlServer.Management.Smo.UserDefinedType>, <xref:Microsoft.SqlServer.Management.Smo.UserDefinedDataType>, and XML types all require a name value to identify the object.  
  
```VBNET
'Declare a DataType object variable and define the data type in the constructor.
Dim dt As DataType
'For the decimal data type the following two arguments specify precision, and scale.
dt = New DataType(SqlDataType.Decimal, 10, 2)
``` 
  
## Constructing a DataType Object with the Specification in the Constructor in Visual C#  
 This code example shows how to use the constructor to create instances of data types that are based on different [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types.  
  
> [!NOTE]  
>  The <xref:Microsoft.SqlServer.Management.Smo.UserDefinedType>, <xref:Microsoft.SqlServer.Management.Smo.UserDefinedDataType>, and XML types all require a name value to identify the object.  
  
```csharp  
{   
//Declare a DataType object variable and define the data type in the constructor.   
DataType dt;   
//For the decimal data type the following two arguments specify precision, and scale.   
dt = new DataType(SqlDataType.Decimal, 10, 2);   
}  
```  
  
## Constructing a DataType Object by Using the Default Constructor in Visual Basic  
 This code example shows how to use the default constructor to create instances of data types that are based on different [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types. The properties are then used to specify the data type.  
  
 **Note** The <xref:Microsoft.SqlServer.Management.Smo.UserDefinedType>, <xref:Microsoft.SqlServer.Management.Smo.UserDefinedDataType>, and XML types all require a name value to identify the object.  
  
```VBNET
'Declare and create a DataType object variable.
Dim dt As DataType
dt = New DataType
'Define the data type by setting the SqlDataType property.
dt.SqlDataType = SqlDataType.VarChar
'The VarChar data type requires a value for the MaximumLength property.
dt.MaximumLength = 100
```
  
## Constructing a DataType Object by Using the Default Constructor in Visual C#  
 This code example shows how to use the default constructor to create instances of data types that are based on different [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types. The properties are then used to specify the data type.  
  
 **Note** The <xref:Microsoft.SqlServer.Management.Smo.UserDefinedType>, <xref:Microsoft.SqlServer.Management.Smo.UserDefinedDataType>, and XML types all require a name value to identify the object.  
  
```csharp  
{   
//Declare and create a DataType object variable.   
DataType dt;   
dt = new DataType();   
//Define the data type by setting the SqlDataType property.   
dt.SqlDataType = SqlDataType.VarChar;   
//The VarChar data type requires a value for the MaximumLength property.   
dt.MaximumLength = 100;   
}  
```  
  
  
