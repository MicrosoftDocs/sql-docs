---
title: "Deploy Packages to Integration Services Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c26ce7f4-7b34-4c9a-8649-ba767d30c827
caps.latest.revision: 10
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Deploy Packages to Integration Services Server
  The Incremental Package Deployment feature introduced in  [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] lets you deploy one or more packages to an existing or new project without deploying the whole project.  
  
##  <a name="DeployWizard"></a> Deploy packages by using the Integration Services Deployment Wizard  
  
1.  From the command prompt, run **isdeploymentwizard.exe** from **%ProgramFiles%\Microsoft SQL Server\130\DTS\Binn**. On 64-bit computers, there is also a 32-bit version of the tool in **%ProgramFiles(x86)%\Microsoft SQL Server\130\DTS\Binn**.  
  
2.  On the **Select Source** page, switch to **Package Deployment model**. Then, select the folder which contains source packages and configure the packages.  
  
3.  Complete the wizard. Follow the remaining steps described in [Package Deployment Model](../../integration-services/packages/integration-services-deployment-wizard.md#PackageModel).  
  
##  <a name="SSMS"></a> Deploy packages by using SQL Server Management Studio  
  
1.  In SQL Server Management Studio, expand the **Integration Services Catalogs** > **SSISDB** node in Object Explorer.  
  
2.  Right-click the **Projects** folder, and then click **Deploy Projects**.  
  
3.  If you see the **Introduction** page, click **Next** to continue.  
  
4.  On the **Select Source** page, switch to **Package Deployment model**. Then, select the folder which contains source packages and configure the packages.  
  
5.  Complete the wizard. Follow the remaining steps described in [Package Deployment Model](../../integration-services/packages/integration-services-deployment-wizard.md#PackageModel).  
  
##  <a name="SSDT"></a> Deploy packages by using SQL Server Data Tools (Visual Studio)  
  
1.  In Visual Studio, with an Integration Services project open, select the package or packages that you want to deploy.  
  
2.  Right-click and select **Deploy Package**. The Deployment Wizard opens with the selected packages configured as the source packages.  
  
3.  Complete the wizard. Follow the remaining steps described in [Package Deployment Model](../../integration-services/packages/integration-services-deployment-wizard.md#PackageModel).  
  
##  <a name="StoredProcedure"></a> Deploy packages by using the deploy_packages stored procedure  
 You can use the **[catalog].[deploy_packages]** stored procedure to deploy one or more SSIS packages to the SSIS Catalog. The following code example demonstrates the use of this stored procedure to deploy packages to an SSIS server. For more info, see [catalog.deploy_packages](../../integration-services/system-stored-procedures/catalog-deploy-packages.md).  
  
```  
  
private static void Main(string[] args)  
{  
    // Connection string to SSISDB  
    var connectionString = "Data Source=.;Initial Catalog=SSISDB;Integrated Security=True;MultipleActiveResultSets=false";  
  
    using (var sqlConnection = new SqlConnection(connectionString))  
    {  
        sqlConnection.Open();  
  
        var sqlCommand = new SqlCommand  
        {  
            Connection = sqlConnection,  
            CommandType = CommandType.StoredProcedure,  
            CommandText = "[catalog].[deploy_packages]"  
        };  
  
        var packageData = Encoding.UTF8.GetBytes(File.ReadAllText(@"C:\Test\Package.dtsx"));  
  
        // DataTable: name is the package name without extension and package_data is byte array of package.  
        var packageTable = new DataTable();  
        packageTable.Columns.Add("name", typeof(string));  
        packageTable.Columns.Add("package_data", typeof(byte[]));  
        packageTable.Rows.Add("Package", packageData);  
  
        // Set the destination project and folder which is named Folder and Project.  
        sqlCommand.Parameters.Add(new SqlParameter("@folder_name", SqlDbType.NVarChar, ParameterDirection.Input, "Folder", -1));  
        sqlCommand.Parameters.Add(new SqlParameter("@project_name", SqlDbType.NVarChar, ParameterDirection.Input, "Project", -1));  
        sqlCommand.Parameters.Add(new SqlParameter("@packages_table", SqlDbType.Structured, ParameterDirection.Input, packageTable, -1));  
  
        var result = sqlCommand.Parameters.Add("RetVal", SqlDbType.Int);  
        result.Direction = ParameterDirection.ReturnValue;  
  
        sqlCommand.ExecuteNonQuery();  
    }  
}  
  
```  
  
##  <a name="MOMApi"></a> Deploy packages using the Management Object Model API  
 The following code example demonstrates the use of the Management Object Model API to deploy packages to server.  
  
```  
  
static void Main()  
 {  
     // Before deploying packages, make sure the destination project exists in SSISDB.  
     var connectionString = "Data Source=.;Integrated Security=True;MultipleActiveResultSets=false";  
     var catalogName = "SSISDB";  
     var folderName = "Folder";  
     var projectName = "Project";  
  
     // Get the folder instance.  
     var sqlConnection = new SqlConnection(connectionString);  
     var store = new Microsoft.SqlServer.Management.IntegrationServices.IntegrationServices(sqlConnection);  
     var folder = store.Catalogs[catalogName].Folders[folderName];  
  
     // Key is package name without extension and value is package binaries.  
     var packageDict = new Dictionary<string, string>();  
  
     var packageData = File.ReadAllText(@"C:\Folder\Package.dtsx");  
     packageDict.Add("Package", packageData);  
  
     // Deploy package to the destination project.  
     folder.DeployPackages(projectName, packageDict);  
 }  
  
```  
  
  