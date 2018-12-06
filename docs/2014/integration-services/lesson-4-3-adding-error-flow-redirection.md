---
title: "Step 3: Adding Error Flow Redirection | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 5683a45d-9e73-4cd5-83ca-fae8b26b488c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 3: Adding Error Flow Redirection
  As demonstrated in the previous task, the Lookup Currency Key transformation cannot generate a match when the transformation tries to process the corrupted sample flat file, which produced an error. Because the transformation uses the default settings for error output, any error causes the transformation to fail. When the transformation fails, the rest of the package also fails.  
  
 Instead of permitting the transformation to fail, you can configure the component to redirect the failed row to another processing path by using the error output. Use of a separate error processing path lets you do a number of things. For instance, you might try to clean the data and then reprocess the failed row. Or, you might save the failed row along with additional error information for later verification and reprocessing.  
  
 In this task, you will configure the Lookup Currency Key transformation to redirect any rows that fail to the error output. In the error branch of the data flow, these rows will be written to a file.  
  
 By default the two extra columns in an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] error output, **ErrorCode** and **ErrorColumn**, contain only numeric codes that represent an error number, and the ID of the column in which the error occurred. These numeric values may be of limited use without the corresponding error description.  
  
 To enhance the usefulness of the error output, before the package writes the failed rows to the file, you will use a Script component to access the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] API and get a description of the error.  
  
### To configure an error output  
  
1.  In the **SSIS Toolbox**, expand **Common**, and then drag **Script Component** onto the design surface of the **Data Flow** tab. Place **Script** to the right of the **Lookup Currency Key** transformation.  
  
2.  In the **Select Script Component Type** dialog box, click **Transformation**, and click **OK**.  
  
3.  Click the **Lookup Currency Key** transformation and then drag the red arrow onto the newly added **Script** transformation to connect the two components.  
  
     The red arrow represents the error output of the **Lookup Currency Key** transformation. By using the red arrow to connect the transformation to the Script component, you can redirect any processing errors to the Script component, which then processes the errors and sends them to the destination.  
  
4.  In the **Configure Error Output** dialog box, in the **Error** column, select **Redirect row**, and then click **OK**.  
  
5.  On the **Data Flow** design surface, click **Script Component** in the newly added **ScriptComponent**, and change the name to **Get Error Description**.  
  
6.  Double-click the **Get Error Description** transformation.  
  
7.  In the **Script Transformation Editor** dialog box, on the **Input Columns** page, select the **ErrorCode** column.  
  
8.  On the **Inputs and Outputs** page, expand **Output 0**, click **Output Columns**, and then click **Add Column**.  
  
9. In the `Name` property, type **ErrorDescription** and set the `DataType` property to **Unicode string [DT_WSTR]**.  
  
10. On the **Script** page, verify that the `LocaleID` property is set to **English (United States.**  
  
11. Click **Edit Script** to open [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] Tools for Applications (VSTA). In the `Input0_ProcessInputRow` method, type or paste the following code.  
  
     [Visual Basic]  
  
    ```  
    Row.ErrorDescription =   
      Me.ComponentMetaData.GetErrorDescription(Row.ErrorCode)  
    ```  
  
     [Visual C#]  
  
    ```  
    Row.ErrorDescription = this.ComponentMetaData.GetErrorDescription(Row.ErrorCode);  
    ```  
  
     The completed subroutine will look like the following code.  
  
     [Visual Basic]  
  
    ```  
    Public Overrides Sub Input0_ProcessInputRow(ByVal Row As Input0Buffer)  
  
      Row.ErrorDescription =   
        Me.ComponentMetaData.GetErrorDescription(Row.ErrorCode)  
  
    End Sub  
    ```  
  
     [Visual C#]  
  
    ```  
    public override void Input0_ProcessInputRow(Input0Buffer Row)  
        {  
  
            Row.ErrorDescription = this.ComponentMetaData.GetErrorDescription(Row.ErrorCode);  
  
        }  
    ```  
  
12. On the **Build** menu, click **Build Solution** to build the script and save your changes, and then close VSTA.  
  
13. Click **OK** to close the **Script Transformation Editor** dialog box.  
  
## Next Steps  
 [Step 4: Adding a Flat File Destination](lesson-4-4-adding-a-flat-file-destination.md  
  
  
