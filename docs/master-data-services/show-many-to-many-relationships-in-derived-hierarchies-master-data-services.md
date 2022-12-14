---
description: "Show Many-to-Many Relationships in Derived Hierarchies (Master Data Services)"
title: Show Many-to-Many Relationships in Derived Hierarchies
ms.custom: "seo-lt-2019"
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 8b2a9c43-40e0-48f7-a6a9-325beb9f27da
author: CordeliaGrey
ms.author: jiwang6
---
# Show Many-to-Many Relationships in Derived Hierarchies (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Derived Hierarchies (DH) display one-to-many relationships, and can now also show many-to-many relationships.  
  
## Many-to-Many (M2M) Relationships  

A many-to-many (M2M) relationship between two entities may be modeled through the use of a third entity that provides a mapping between them:  
  
![mds_hierarchies_manytomany](../master-data-services/media/mds-hierarchies-manytomany.png "mds_hierarchies_manytomany")  
  
In the above example, there is an M2M relationship between the **Employee** and **TrainingClass** entities, provided by the mapping entity **ClassRegistration**. An employee may be registered as a student in multiple classes, and each class may contain multiple students.  
  
You can create a derived hierarchy that displays, for example, students by class, or invert the relationship and show classes grouped by student.  

> [!NOTE]
> [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] introduced derived hierarchy for M2M relationships. This capability was not available prior to that version.
  
First, go to the derived hierarchy management page and create a new derived hierarchy:  
  
 ![mds_hierarchies_add_derived_hierarchy](../master-data-services/media/mds-hierarchies-add-derived-hierarchy.png "mds_hierarchies_add_derived_hierarchy")  
  
 Next, add levels to the new derived hierarchy, starting from the bottom up. In this example, we wish to show students (employees) grouped by class. The **Employee** entity is therefore the leaf level of the hierarchy, and is added first:  
  
 ![mds_hierarchies_edit_derived_hierarchy_one](../master-data-services/media/mds-hierarchies-edit-derived-hierarchy-one.PNG "mds_hierarchies_edit_derived_hierarchy_one")  
  
 In the above screenshot, note that the **Employee** entity appears under **Current Levels** in the middle as the only level. The derived hierarchy **Preview** on the right simply shows a list of all members of the **Employee** entity. The **Available Levels** section on the left shows what levels may be added on top of the current top level (**Employee**). Most of these are domain-based attributes (DBAs) on the **Employee** entity, including the **Department** DBA.  
  
 Beginning with [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)], there is a new type of level that models M2M relationships, for example: **Class (mapped via ClassRegistration.Student)**. The level name is more verbose than the others to reflect the extra information needed to unambiguously describe the mapping relationship. Drag and drop this level to the **Employee** level in the **Current Levels** section:  
  
 ![mds_hierarchies_edit_derived_hierarchy_two](../master-data-services/media/mds-hierarchies-edit-derived-hierarchy-two.PNG "mds_hierarchies_edit_derived_hierarchy_two")  
  
 Now the preview shows employees grouped by the training classes for which they are registered. Since this is a M2M relationship, each child member can have multiple parents. In the above example, employee **6 {Hillman, Reinout N}** is registered as a student in two classes, **1 {Master Data Services 101}** and **4 {Career-Limiting Moves}**.  
  
 This mapping relationship can also be displayed inverted, grouping classes by student:  
  
 ![mds_hierarchies_available_entities_and_hierarchies](../master-data-services/media/mds-hierarchies-available-entities-and-hierarchies.PNG "mds_hierarchies_available_entities_and_hierarchies")  
  
 Again, we see how a child can appear under more than one parent: training class **1 {Master Data Services 101}** appears under both **6 {Hillman, Reinout N}** and **40 {Ford, Jeffrey L}**.  
  
 The members of the mapping entity **ClassRegistration** do not appear anywhere within the derived hierarchy. They are used merely to define the relationships between parent and child members in the hierarchy.  
  
 You can edit the M2M relationship by modifying the mapping entity members, by doing one of the following. The M2M relationship is read-only in the **Derived Hierarchy Explorer** page.  
  
-   Modify the mapping entity members in the **Entity Explorer** page, using the Master Data Services add-in for Excel, or by using data staging.  
  
-   Drag and drop child nodes between parents in the **Derived Hierarchy Explorer** page.  
  
     This method modifies  existing members when possible and adds new members when necessary. Existing members are not deleted.  
  
     For example, with the ClassRegistration mapping entity, when moving a student to the unused node, the class attribute value of the corresponding mapping entity member is changed to null, and the member is not deleted. Conversely, when moving a student from the unused node to some class, if there exists an existing mapping member corresponding to the student where class is null, that member is modified by changing class from null to the new parent. If no such member is found, then one is added.  
  
     This process avoids member deletion to prevent unwanted deletion of other user data, for example if the mapping entity contains other attributes besides the two that define the parent-child relationship. Users must explicitly do deletions directly on the mapping entity.  
  
 The new M2M level can appear anywhere within a derived hierarchy that a domain-based attribute (DBA) level is allowed. A M2M level can be at the top, like in the above examples. It can be above and/or under a DBA level, including recursive levels. It can be under an Explicit Hierarchy (deprecated) Cap level. Multiple M2M relationships can be chained together in the same derived hierarchy.  
  
 M2M levels may be hidden, just like other derived hierarchy levels.  
   
### <a name="M2MSample"></a> M2M Relationship in Sample Model  
For a demonstration of an M2M relationship, view the Region Climate derived hierarchy in the Customer sample model that is included with [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)].   
  
As shown in the following image, the level name that models this relationship is ![mds_Number1](../master-data-services/media/mds-number1.png)**Climate (mapped via RegionClimate.Region)**. The ![mds_Number2](../master-data-services/media/mds-number2.png)**Preview** shows regions grouped by the types of climates that they are associated with. This is a M2M relationship because there are regions (child members) that are associated with multiple climates (parents). For example, ![mds_Number3](../master-data-services/media/mds-number3.png)**APCR {Asia Pacific}** is associated with ![mds_Number4](../master-data-services/media/mds-number4.png)**A {Tropical}** and ![mds_Number5](../master-data-services/media/mds-number5.png)**B {Dry}**.  
  
![mds_M2MRelationship_Example_CustomerModel](../master-data-services/media/mds-m2mrelationship-example-customermodel.png)  
  
For instructions on deploying the Customer sample model, and other sample models included with [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)], see [Deploying Sample Models and Data](~/master-data-services/sql-server-samples-model-deployment-packages-mds.md).   
  
## One-Many Relationship  
 A member of a DH may be the parent of many child members, but it generally cannot have more than one parent (for exceptions, see [Member Security](#bkmk_member_security)). For example, suppose there are two entities: Employee and Department, where each employee belongs to a single department. This relationship is modeled by adding to the Employee entity a domain-based attribute (DBA) that references the Department entity:  
  
 ![mds_hierarchies_onetomany](../master-data-services/media/mds-hierarchies-onetomany.png "mds_hierarchies_onetomany")  
  
 This is a one-to-many relationship because each employee belongs to just one department, but each department can have multiple employees. A derived hierarchy may be created that displays employees grouped by department:  
  
 ![mds_hierarchies_dh_screenshot](../master-data-services/media/mds-hierarchies-dh-screenshot.png "mds_hierarchies_dh_screenshot")  
  
##  <a name="bkmk_member_security"></a> Member Security  
 A hierarchy that allows member duplication (allows a member to have more than one parent) cannot be used to assign member security permissions. For example:  
  
-   A Recursive derived hierarchy (RDH) that does not anchor null recursions (each member at the recursive level appears under both ROOT and its recursive parent).  
  
-   A Recursive derived hierarchy with a level above the recursive level (each member of the recursive level appears under both its non-recursive parent and its recursive parent).  
  
-   A derived hierarchy with a M2M level (a child may be mapped to many parents).  
  
## Collections  
 Collections and Explicit Hierarchies are deprecated. The conversion stored procedure (udpConvertCollectionAndConsolidatedMembersToLeaf) converts collection members to leaf members and creates many-to-many Derived Hierarchies to capture collection membership info.  
  
## See Also  
 [Derived Hierarchies &#40;Master Data Services&#41;](../master-data-services/derived-hierarchies-master-data-services.md)  
  
  
