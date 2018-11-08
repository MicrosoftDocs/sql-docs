---
title: "Grant cube or model permissions (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.roledesignerdialog.cubes.f1"
helpviewer_keywords: 
  - "user access rights [Analysis Services], cubes"
  - "cubes [Analysis Services], security"
  - "read/write permissions"
  - "permissions [Analysis Services], cubes"
ms.assetid: 55b1456e-2f6b-4101-b316-c926f40304e3
author: minewiskan
ms.author: owend
manager: craigg
---
# Grant cube or model permissions (Analysis Services)
  A cube or tabular model is the primary query object in an Analysis Services data model. When connecting to multidimensional or tabular data from Excel for ad hoc data exploration, users typically start by selecting a specific cube or tabular model as the data structure behind the Pivot report object. This topic explains how to grant the necessary permissions for cube or tabular data access.  
  
 By default, no one except a Server Administrator or Database Administrator has permission to query cubes in a database. Cube access by a non-administrator requires membership in a role created for the database containing the cube. Membership is supported for Windows user or group accounts, defined in either Active Directory or on the local computer. Before you start, identify which accounts will be assigned membership in the roles you are about to create.  
  
 Having `Read` access to a cube also conveys permissions on the dimensions, measure groups, and perspectives within it. Most administrators will grant read permissions at the cube level and then restrict permissions on specific objects, on associated data, or by user identity.  
  
 To preserve role definitions over successive solution deployments, a best practice is to define roles in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] as an integral part of the model, and then have a database administrator assign role memberships in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] after the database is published. But you can use either tool for both tasks. To simplify the exercise, we'll use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] for both role definition and membership.  
  
> [!NOTE]  
>  Only server administrators, or database administrators having Full Control permissions, can deploy a cube from source files to a server, or create roles and assign members. See [Grant Server Administrator Permissions &#40;Analysis Services&#41;](../instances/grant-server-admin-rights-to-an-analysis-services-instance.md) and [Grant database permissions &#40;Analysis Services&#41;](grant-database-permissions-analysis-services.md) for details about these permission levels.  
  
#### Step 1: Create the role  
  
1.  In SSMS, connect to Analysis Services. See [Connect from client applications &#40;Analysis Services&#41;](../instances/connect-from-client-applications-analysis-services.md) if you need help with this step.  
  
2.  Open the **Databases** folder in Object Explorer, and select a database.  
  
3.  Right-click **Roles** and choose **New Role**. Notice that roles are created at the database level and apply to objects within it. You cannot share roles across databases.  
  
4.  In the **General** pane, enter a name, and optionally, a description. This pane also contains several database permissions, such as Full Control, Process Database, and Read Definition. None of these permissions are needed for querying a cube or tabular model. See [Grant database permissions &#40;Analysis Services&#41;](grant-database-permissions-analysis-services.md) for more information about these permissions.  
  
5.  Continue to the next step after entering a name and optional description.  
  
#### Step 2: Assign Membership  
  
1.  In the **Membership** pane, click **Add** to enter the Windows user or group accounts that will be accessing the cube using this role. Analysis Services only supports Windows security identities. Notice that you are not creating database logins in this step. In Analysis Services, users connect through Windows accounts.  
  
2.  Continue to the next step, setting cube permissions.  
  
     Notice that we are skipping the Data Source pane. Most regular consumers of Analysis Services data do not need permissions on the data source object. See [Grant permissions on a data source object &#40;Analysis Services&#41;](grant-permissions-on-a-data-source-object-analysis-services.md) for details on when you might set this permission.  
  
#### Step 3: Set Cube Permissions  
  
1.  In the **Cubes** pane, select a cube, and then click `Read` or **Read/Write** access.  
  
     `Read` access is sufficient for most operations. **Read/Write** is used only for writeback, not processing. See [Set Partition Writeback](set-partition-writeback.md) for more information about this capability.  
  
     Notice that you can select multiple cubes, as well as other objects available in the Create Role dialog box. Granting permissions to a cube authorizes access to the dimensions and perspectives associated with the cube. It's not necessary to manually add objects already represented in the cube.  
  
     If you need to vary authorization by objects or user, for example to make certain measures unavailable, you can allow or deny access atomically on specific objects, even on cells. See [Grant custom access to dimension data &#40;Analysis Services&#41;](grant-custom-access-to-dimension-data-analysis-services.md) and [Grant custom access to cell data &#40;Analysis Services&#41;](grant-custom-access-to-cell-data-analysis-services.md) for details.  
  
2.  At this point, after you click **OK**, all members of this role have access to the cubes, at the permission levels you specified.  
  
     Notice that on the **Cubes** pane, you can grant users permission to create local cubes from a server cube via **Drillthrough and Local Cube**, or allow drillthrough only, via the **Drillthrough** permission.  
  
     Finally, this pane lets you grant **Process Database** rights on the cube to give all members of this role the ability to process data for this cube. Because processing is typically a restricted operation, we recommend that you leave that task to the administrators, or define separate roles specifically for that task. See [Grant process permissions &#40;Analysis Services&#41;](grant-process-permissions-analysis-services.md) for more information about processing permission best practices.  
  
#### Step 4: Test  
  
1.  Use Excel to test cube access permissions. You can also use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], following the same technique described next ─ running the application as a non-administrator user.  
  
    > [!NOTE]  
    >  If you are an Analysis Services administrator, administrator permissions will be combined with roles having lesser permissions, making it difficult to test role permissions in isolation. To simplify testing, we suggest that you open a second instance of SSMS, using the account assigned to the role you are testing.  
  
2.  Hold-down the Shift key and right-click the **Excel** shortcut to access the **Run as different user** option. Enter one of the Windows user or group accounts having membership in this role.  
  
3.  When Excel opens, use the Data tab to connect to Analysis Services. Because you are running Excel as a different Windows user, the **Use Windows Authentication** option is the right credential type to use when testing roles. See [Connect from client applications &#40;Analysis Services&#41;](../instances/connect-from-client-applications-analysis-services.md) if you need help with this step.  
  
     If you get errors on the connection, check the port configuration for Analysis Services and verify that the server accepts remote connections. See [Configure the Windows Firewall to Allow Analysis Services Access](../instances/configure-the-windows-firewall-to-allow-analysis-services-access.md) for port configuration.  
  
#### Step 5: Script role definition and assignments  
  
1.  As a final step, you should generate a script that captures the role definition you just created.  
  
     Redeploying a project from [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] will overwrite any roles or role memberships that are not defined inside the project. The quickest way to rebuild roles and role membership after redeployment is through script.  
  
2.  In SSMS, navigate to the **Roles** folder and right-click an existing role.  
  
3.  Select **Script Role as** | **CREATE TO** | **file**.  
  
4.  Save the file with an .xmla file extension. To test the script, delete the current role, open the file in SSMS, and press F5 to execute the script.  
  
## Next step  
 You can refine cube permissions to restrict access to cell or dimension data. See [Grant custom access to dimension data &#40;Analysis Services&#41;](grant-custom-access-to-dimension-data-analysis-services.md) and [Grant custom access to cell data &#40;Analysis Services&#41;](grant-custom-access-to-cell-data-analysis-services.md) for details.  
  
## See Also  
 [Authentication methodologies supported by Analysis Services](../instances/authentication-methodologies-supported-by-analysis-services.md)   
 [Grant permissions on data mining structures and models &#40;Analysis Services&#41;](grant-permissions-on-data-mining-structures-and-models-analysis-services.md)   
 [Grant permissions on a data source object &#40;Analysis Services&#41;](grant-permissions-on-a-data-source-object-analysis-services.md)  
  
  
