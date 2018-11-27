---
title: "Warning about client side usage of GEOMETRY, GEOGRAPHY and HIERARCHYID | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 500ee6b3-2154-45d2-a3cf-8760166d9413
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Warning about client side usage of GEOMETRY, GEOGRAPHY and HIERARCHYID
  The assembly **Microsoft.SqlServer.Types.dll**, which contains the spatial data types, has been upgraded from version 10.0 to version 11.0. Custom applications that reference this assembly may fail when certain conditions are true.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 The assembly **Microsoft.SqlServer.Types.dll**, which contains the spatial data types, has been upgraded from version 10.0 to version 11.0. Custom applications that reference this assembly may fail when the following conditions are true.  
  
-   When you move a custom application from a computer on which [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] was installed to a computer on which only [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is installed, the application will fail because the referenced version 10.0 of the **SqlTypes** assembly is not present. You may see this error message: `"Could not load file or assembly 'Microsoft.SqlServer.Types, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91' or one of its dependencies. The system cannot find the file specified."`  
  
-   When you reference the **SqlTypes** assembly version 11.0, and version 10.0 is also installed, you may see this error message: `"System.InvalidCastException: Unable to cast object of type 'Microsoft.SqlServer.Types.SqlGeometry' to type 'Microsoft.SqlServer.Types.SqlGeometry'."`  
  
-   When you reference the **SqlTypes** assembly version 11.0 from a custom application that targets .NET 3.5, 4, or 4.5, the application will fail because SqlClient by design loads version 10.0 of the assembly. This failure occurs when the application calls one of the following methods:  
  
    -   `GetValue` method of the `SqlDataReader` class  
  
    -   `GetValues` method of the `SqlDataReader` class  
  
    -   bracket index operator [] of the `SqlDataReader` class  
  
    -   `ExecuteScalar` method of the `SqlCommand` class  
  
## Corrective Action  
 You can work around this issue by using one of the following methods:  
  
-   You can work around this issue in your code by calling the `GetSqlBytes` method, instead of the Get methods listed above, to retrieve CLR [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system types, as shown in the following example:  
  
    ```csharp  
    string query = "SELECT [SpatialColumn] FROM [SpatialTable]";  
          using (SqlConnection conn = new SqlConnection("..."))  
          {  
                SqlCommand cmd = new SqlCommand(query, conn);  
  
                conn.Open();  
                SqlDataReader reader = cmd.ExecuteReader();  
  
                while (reader.Read())  
                {  
                      // In version 11.0 only  
                      SqlGeometry g =   
    SqlGeometry.Deserialize(reader.GetSqlBytes(0));  
  
                      // In version 10.0 or 11.0  
                      SqlGeometry g2 = new SqlGeometry();  
                      g.Read(new BinaryReader(reader.GetSqlBytes(0).Stream));  
                }  
          }  
    ```  
  
-   You can work around this issue by using assembly redirection in the application configuration file, as shown in the following example:  
  
    ```xml  
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">  
        ...  
        <dependentAssembly>  
            <assemblyIdentity name="Microsoft.SqlServer.Types" publicKeyToken="89845dcd8080cc91" culture="neutral" />  
            <bindingRedirect oldVersion="10.0.0.0" newVersion="11.0.0.0" />  
        </dependentAssembly>  
        ...  
    </assemblyBinding>  
    ```  
  
-   You can work around this issue in your connection string by specifying a value of "SQL Server 2012" for the "Type System Version" attribute to force SqlClient to load version 11.0 of the assembly. This connection string attribute is available only in .NET 4.5 and above.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
