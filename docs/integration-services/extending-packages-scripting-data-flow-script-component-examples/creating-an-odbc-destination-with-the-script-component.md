---
title: "Creating an ODBC Destination with the Script Component | Microsoft Docs"
ms.custom: ""
ms.date: "10/10/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Script component [Integration Services], destination components"
  - "ODBC destination [Integration Services]"
  - "destinations [Integration Services], components"
  - "Script component [Integration Services], examples"
ms.assetid: d198c866-78f4-4a50-ae15-333160645815
author: janinezhang
ms.author: janinez
manager: craigg
---
# Creating an ODBC Destination with the Script Component
  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you typically save data to an ODBC destination by using an [!INCLUDE[vstecado](../../includes/vstecado-md.md)] destination and the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for ODBC. However, you can also create an ad hoc ODBC destination for use in a single package. To create this ad hoc ODBC destination, you use the Script component as shown in the following example.  
  
> [!NOTE]  
>  If you want to create a component that you can more easily reuse across multiple Data Flow tasks and multiple packages, consider using the code in this Script component sample as the starting point for a custom data flow component. For more information, see [Developing a Custom Data Flow Component](../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md).  
  
## Example  
 The following example demonstrates how to create a destination component that uses an existing ODBC connection manager to save data from the data flow into a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 This example is a modified version of the custom [!INCLUDE[vstecado](../../includes/vstecado-md.md)] destination that was demonstrated in the topic, [Creating a Destination with the Script Component](../../integration-services/extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md). However, in this example, the custom [!INCLUDE[vstecado](../../includes/vstecado-md.md)] destination has been modified to work with an ODBC connection manager and save data to an ODBC destination. These modifications also include the following changes:  
  
-   You cannot call the **AcquireConnection** method of the ODBC connection manager from managed code, because it returns a native object. Therefore, this sample uses the connection string of the connection manager to connect to the data source directly by using the managed ODBC [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider.  
  
-   The **OdbcCommand** expects positional parameters. The positions of the parameters are indicated by the question marks (?) in the text of the command. (In contrast, a **SqlCommand** expects named parameters.)  
  
 This example uses the **Person.Address** table in the **AdventureWorks** sample database. The example passes the first and fourth columns, the **int _AddressID_** and **nvarchar(30) _City_** columns, of this table through the data flow. This same data is used in the source, transformation, and destination samples in the topic, [Developing Specific Types of Script Components](../../integration-services/extending-packages-scripting-data-flow-script-component-types/developing-specific-types-of-script-components.md).  
  
#### To configure this Script Component example  
  
1.  Create an ODBC connection manager that connects to the **AdventureWorks** database.  
  
2.  Create a destination table by running the following Transact-SQL command in the **AdventureWorks** database:  
  
    ```sql
    CREATE TABLE [Person].[Address2]([AddressID] [int] NOT NULL,  
        [City] [nvarchar](30) NOT NULL)  
    ```  
  
3.  Add a new Script component to the Data Flow designer surface and configure it as a destination.  
  
4.  Connect the output of an upstream source or transformation to the destination component in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. (You can connect a source directly to a destination without any transformations.) To ensure that this sample works, the output of the upstream component must include at least the **AddressID** and **City** columns from the **Person.Address** table of the **AdventureWorks** sample database.  
  
5.  Open the **Script Transformation Editor**. On the **Input Columns** page, select the **AddressID** and **City** columns.  
  
6.  On the **Inputs and Outputs** page, rename the input with a more descriptive name such as **MyAddressInput**.  
  
7.  On the **Connection Managers** page, add or create the ODBC connection manager with a descriptive name such as **MyODBCConnectionManager**.  
  
8.  On the **Script** page, click **Edit Script**, and then enter the script shown below in the **ScriptMain** class.  
  
9. Close the script development environment, close the **Script Transformation Editor**, and then run the sample.  
  
    ```vb  
    Imports System.Data.Odbc  
    ...  
    Public Class ScriptMain  
        Inherits UserComponent  
  
        Dim odbcConn As OdbcConnection  
        Dim odbcCmd As OdbcCommand  
        Dim odbcParam As OdbcParameter  
  
        Public Overrides Sub AcquireConnections(ByVal Transaction As Object)  
  
            Dim connectionString As String  
            connectionString = Me.Connections.MyODBCConnectionManager.ConnectionString  
            odbcConn = New OdbcConnection(connectionString)  
            odbcConn.Open()  
  
        End Sub  
  
        Public Overrides Sub PreExecute()  
  
            odbcCmd = New OdbcCommand("INSERT INTO Person.Address2(AddressID, City) " & _  
                "VALUES(?, ?)", odbcConn)  
            odbcParam = New OdbcParameter("@addressid", OdbcType.Int)  
            odbcCmd.Parameters.Add(odbcParam)  
            odbcParam = New OdbcParameter("@city", OdbcType.NVarChar, 30)  
            odbcCmd.Parameters.Add(odbcParam)  
  
        End Sub  
  
        Public Overrides Sub MyAddressInput_ProcessInputRow(ByVal Row As MyAddressInputBuffer)  
  
            With odbcCmd  
                .Parameters("@addressid").Value = Row.AddressID  
                .Parameters("@city").Value = Row.City  
                .ExecuteNonQuery()  
            End With  
  
        End Sub  
  
        Public Overrides Sub ReleaseConnections()  
  
            odbcConn.Close()  
  
        End Sub  
  
    End Class  
    ```  
  
    ```csharp  
    using System.Data.Odbc;  
    ...  
    public class ScriptMain :  
        UserComponent  
    {  
        OdbcConnection odbcConn;  
        OdbcCommand odbcCmd;  
        OdbcParameter odbcParam;  
  
        public override void AcquireConnections(object Transaction)  
        {  
  
            string connectionString;  
            connectionString = this.Connections.MyODBCConnectionManager.ConnectionString;  
            odbcConn = new OdbcConnection(connectionString);  
            odbcConn.Open();  
  
        }  
  
        public override void PreExecute()  
        {  
  
            odbcCmd = new OdbcCommand("INSERT INTO Person.Address2(AddressID, City) " +  
                "VALUES(?, ?)", odbcConn);  
            odbcParam = new OdbcParameter("@addressid", OdbcType.Int);  
            odbcCmd.Parameters.Add(odbcParam);  
            odbcParam = new OdbcParameter("@city", OdbcType.NVarChar, 30);  
            odbcCmd.Parameters.Add(odbcParam);  
  
        }  
  
        public override void MyAddressInput_ProcessInputRow(MyAddressInputBuffer Row)  
        {  
  
            {  
                odbcCmd.Parameters["@addressid"].Value = Row.AddressID;  
                odbcCmd.Parameters["@city"].Value = Row.City;  
                odbcCmd.ExecuteNonQuery();  
            }  
  
        }  
  
        public override void ReleaseConnections()  
        {  
  
            odbcConn.Close();  
  
        }  
    }  
    ```  
  
## See Also  
 [Creating a Destination with the Script Component](../extending-packages-scripting-data-flow-script-component-types/creating-a-destination-with-the-script-component.md)  
  
  
