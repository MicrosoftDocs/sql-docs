---
title: "Analysis Services Scripting Language XML Element Hierarchy (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Analysis Services Scripting Language XML Element Hierarchy"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "Analysis Services Scripting Language, elements"
  - "elements [Analysis Services Scripting Language]"
  - "ASSL, elements"
  - "objects [Analysis Services Scripting Language]"
  - "hierarchies [Analysis Services Scripting Language]"
ms.assetid: 343dbab9-4c2c-49b9-8f35-efc65f2216f1
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Analysis Services Scripting Language XML Element Hierarchy (ASSL)
  The following table displays the hierarchy of objects in the Analysis Services Scripting Language (ASSL).  
  
## Syntax  
  
```xml  
  
<Server>  
   <Name/>  
   <ID/>  
   <Description/>  
   <Version/>  
   <Edition/>  
   <Databases>  
      <Database>  
         <Name/>  
         <ID/>  
                  <CreatedTimestamp/>  
                  <LastSchemaUpdate/>  
         <Description/>  
         <AggregationPrefix/>  
                  <EstimatedSize/>  
         <LastProcessed/>  
         <Language/>  
                  <Collation/>  
                  <Visible/>  
         <Dimensions>  
            <Dimension>  
                              <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <Description/>  
               <Source>  
                  <!-- Binding -->  
               </Source>  
               <MiningModelID/>  
                              <Type/>  
               <UnknownMember/>  
               <ErrorConfiguration>  
                                    <KeyErrorLimit/>  
                  <KeyErrorLogFile/>  
                  <KeyErrorAction/>  
                  <KeyErrorLimitAction/>  
                  <KeyNotFound/>  
                  <KeyDuplicate/>  
                  <NullKeyConvertedToUnknown/>  
                  <NullKeyNotAllowed/>  
               </ErrorConfiguration>  
               <StorageMode/>  
               <WriteEnabled/>  
               <LastProcessed/>  
               <DimensionPermissions>  
                  <DimensionPermission xsi:type="Permission">  
                     <Name/>  
                     <ID/>  
                     <CreatedTimestamp/>  
                                          <LastSchemaUpdate/>  
                     <RoleID/>  
                     <Description/>  
                                          <Process/>  
                     <ReadDefinition/>  
                                          <Access/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                                          <AttributePermissions>  
                        <AttributePermission>  
                           <AttributeID/>  
                           <Description/>  
                           <DefaultMember/>  
                           <VisualTotals/>  
                                                      <AllowedSet/>  
                           <DeniedSet/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                                                  <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                </AttributePermission>  
                     </AttributePermissions>  
                  </DimensionPermission>  
               </DimensionPermissions>  
               <DependsOnDimensionID/>  
               <Language/>  
               <Collation/>  
                              <UnknownMemberName/>  
               <UnknownMemberTranslations>  
                  <UnknownMemberTranslation xsi:type="Translation">  
                     <Language/>  
                                          <Caption/>  
                     <Description/>  
                     <DisplayFolder/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                  </UnknownMemberTranslation>  
               </UnknownMemberTranslations>  
               <State/>  
               <ProactiveCaching>  
                  <OnlineMode/>  
                  <AggregationStorage/>  
                  <Source xsi:type="ProactiveCachingBinding">  
                     <!-- Binding -->  
                  </Source>  
                  <SilenceInterval/>  
                  <Latency/>  
                  <SilenceOverrideInterval/>  
                  <ForceRebuildInterval/>  
                  <Enabled/>  
               </ProactiveCaching>  
               <ProcessingMode/>  
               <CurrentStorageMode/>  
               <Translations>  
                                    <Translation>  
                                        <Language/>  
                                          <Caption/>  
                     <Description/>  
                     <DisplayFolder/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                   </Translation>  
               </Translations>  
               <Attributes>  
                  <Attribute xsi:type="DimensionAttribute">  
                     <Name/>  
                                          <ID/>  
                     <Description/>  
                     <Type/>  
                     <Usage/>  
                                          <Source>  
                        <!-- Binding -->  
                     </Source>  
                     <EstimatedCount/>  
                     <KeyColumns>  
                        <KeyColumn xsi:type="DataItem">  
                           <!-- DataItem -->  
                        </KeyColumn>  
                     </KeyColumns>  
                     <NameColumn xsi:type="DataItem">  
                        <!-- DataItem -->  
                     </NameColumn>  
                     <Translations>  
                                                <Translation xsi:type="AttributeTranslation">  
                                                      <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                           <CaptionColumn xsi:type="DataItem">  
                              <!-- DataItem -->  
                           </CaptionColumn>  
                           <MembersWithDataCaption/>  
                        </Translation>  
                     </Translations>  
                     <MemberProperties>  
                                                <MemberProperty>  
                           <AttributeID/>  
                                                      <RelationshipType/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                           <Name/>  
                           <Visible/>  
                           <Translations>  
                              <Language/>  
                                                            <Caption/>  
                              <Description/>  
                              <DisplayFolder/>  
                              <Annotations>  
                                 <Annotation>  
                                    <Name/>  
                                    <Visibility/>  
                                    <Value/>  
                                                                        <!-- User-defined elements -->  
                                 </Annotation>  
                              </Annotations>  
                           </Translations>  
                        </MemberProperty>  
                     </MemberProperties>  
                     <DiscretizationMethod/>  
                     <DiscretizationBucketCount/>  
                     <RootMemberIf/>  
                     <OrderBy/>  
                     <DefaultMember/>  
                     <OrderByAttributeID/>  
                     <SkippedLevelsColumn xsi:type="DataItem">  
                        <!-- DataItem -->  
                     </SkippedLevelsColumn>  
                     <NamingTemplate/>  
                     <MembersWithData/>  
                     <MembersWithDataCaption/>  
                     <NamingTemplateTranslations>  
                        <NamingTemplateTranslation xsi:type="Translation">  
                           <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </NamingTemplateTranslation>  
                     </NamingTemplateTranslations>  
                     <CustomRollupColumn xsi:type="DataItem">  
                        <!-- DataItem -->  
                     </CustomRollupColumn>  
                     <CustomRollupPropertiesColumn xsi:type="DataItem">  
                        <!-- DataItem -->  
                     </CustomRollupPropertiesColumn>  
                     <UnaryOperatorColumn xsi:type="DataItem">  
                                                <!-- DataItem -->  
                     </UnaryOperatorColumn>  
                     <AttributeHierarchyOrdered/>  
                                          <MemberNamesUnique/>  
                     <IsAggregatable/>  
                                          <AttributeHierarchyEnabled/>  
                     <AttributeHierarchyOptimizedState/>  
                     <AttributeHierarchyVisible/>  
                     <AttributeHierarchyDisplayFolder/>  
                     <KeyUniquenessGuarantee/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                  </Attribute>  
               </Attributes>  
               <AttributeAllMemberName/>  
               <AttributeAllMemberTranslations>  
                  <AttributeAllMemberTranslation xsi:type="Translation">  
                    <Language/>  
                     <Caption/>  
                                          <Description/>  
                     <DisplayFolder/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                   </AttributeAllMemberTranslation>  
               </AttributeAllMemberTranslations>  
               <Hierarchies>  
                                    <Hierarchy>  
                     <Name/>  
                     <ID/>  
                     <Description/>  
                     <DisplayFolder/>  
                                          <Translations>  
                                                <Translation>  
                                                      <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Translation>  
                     </Translations>  
                     <AllMemberName/>  
                     <AllMemberTranslations>  
                        <AllMemberTranslation xsi:type"Translation">  
                           <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </AllMemberTranslation>  
                     </AllMemberTranslations>  
                     <MemberNamesUnique/>  
                     <AllowDuplicateNames/>  
                                          <Levels>  
                                                <Level>  
                           <Name/>  
                           <ID/>  
                           <Description/>  
                           <SourceAttributeID/>  
                           <HideMemberIf/>  
                           <Translations>  
                                                            <Translation>  
                                                                  <Language/>  
                                                                  <Caption/>  
                                 <Description/>  
                                 <DisplayFolder/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </Translation>  
                           </Translations>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Level>  
                     </Levels>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                                    </Hierarchy>  
               </Hierarchies>  
               <Annotations>  
                                    <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
                        </Dimension>  
         </Dimensions>  
         <Cubes>  
                        <Cube>  
                              <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <Description/>  
               <Language/>  
               <Collation/>  
               <Translations>  
                                    <Translation>  
                                        <Language/>  
                                          <Caption/>  
                     <Description/>  
                     <DisplayFolder/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                   </Translation>  
               </Translations>  
               <Dimensions>  
                                    <Dimension xsi:type="CubeDimension">  
                     <ID/>  
                     <Name/>  
                     <Translations>  
                        <Translation>  
                          <Language/>  
                           <Caption/>  
                                                      <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                         </Translation>  
                     </Translations>  
                     <DimensionID/>  
                     <Visible/>  
                     <AllMemberAggregationUsage/>  
                     <Attributes>  
                                                <Attribute xsi:type="CubeAttribute">  
                           <AttributeID/>  
                           <AggregationUsage/>  
                           <AttributeHierarchyOptimizedState/>  
                           <AttributeHierarchyEnabled/>  
                           <AttributeHierarchyVisible/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                </Attribute>  
                     </Attributes>  
                     <Hierarchies>  
                        <Hierarchy xsi:type="CubeHierarchy">  
                           <HierarchyID/>  
                                                      <Name/>  
                           <OptimizedState/>  
                           <Visible/>  
                           <Enabled/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                </Hierarchy>  
                     </Hierarchies>  
                     <Annotations>  
                                                <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                  </Dimension>  
               </Dimensions>  
               <CubePermissions>  
                  <CubePermission xsi:type="Permission">  
                     <Name/>  
                     <ID/>  
                     <CreatedTimestamp/>  
                                          <LastSchemaUpdate/>  
                     <RoleID/>  
                     <Description/>  
                                          <Process/>  
                     <ReadDefinition/>  
                                          <Access/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                     <DimensionPermissions>  
                        <DimensionPermission xsi:type="CubeDimensionPermission">  
                                                      <CubeDimensionID/>  
                           <Description/>  
                           <Access/>  
                           <AttributePermissions>  
                              <AttributePermission>  
                                 <AttributeID/>  
                                 <Description/>  
                                 <DefaultMember/>  
                                 <VisualTotals/>  
                                                                  <AllowedSet/>  
                                 <DeniedSet/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                                                              <Value/>  
                                       <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </AttributePermission>  
                           </AttributePermissions>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                </DimensionPermission>  
                     </DimensionPermissions>  
                     <CellPermissions>  
                                                <CellPermission>  
                           <Access/>  
                           <Description/>  
                           <Expression/>  
                           <Annotations>  
                                                            <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </CellPermission>  
                     </CellPermissions>  
                  </CubePermission>  
               </CubePermissions>  
               <MdxScripts>  
                  <MdxScript>  
                     <Name/>  
                     <ID/>  
                     <CreatedTimestamp/>  
                     <LastSchemaUpdate/>  
                     <Description/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                     <Commands>  
                        <Command>  
                           <Text/>  
                        </Command>  
                     </Commands>  
                     <DefaultScript/>  
                     <CalculationProperties>  
                        <CalculationProperty>  
                           <CalculationReference/>  
                           <CalculationType/>  
                           <Translations>  
                              <Translation>  
                                 <Language/>  
                                                                  <Caption/>  
                                 <Description/>  
                                 <DisplayFolder/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </Translation>  
                           </Translations>  
                           <Description/>  
                           <Visible/>  
                           <SolveOrder/>  
                           <FormatString/>  
                           <ForeColor/>  
                           <BackColor/>  
                           <FontName/>  
                           <FontSize/>  
                           <FontFlags/>  
                           <NonEmptyBehavior/>  
                           <AssociatedMeasureGroupID/>  
                                                      <DisplayFolder/>  
                                                </CalculationProperty>  
                     </CalculationProperties>  
                  </MdxScript>  
               </MdxScripts>  
               <Perspectives>  
                                    <Perspective>  
                     <Name/>  
                                          <ID/>  
                     <CreatedTimestamp/>  
                     <LastSchemaUpdate/>  
                     <Description/>  
                     <Translations>  
                        <Translation>  
                          <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                         </Translation>  
                     </Translations>  
                     <Dimensions>  
                                                <Dimension xsi:type="PerspectiveDimension">  
                                                      <CubeDimensionID/>  
                           <Attributes>  
                              <Attribute xsi:type="PerspectiveAttribute">  
                                 <AttributeID/>  
                                 <DefaultMember/>  
                                 <AttributeHierarchyVisible/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                                                            </Attribute>  
                           </Attributes>  
                           <Hierarchies>  
                              <Hierarchy xsi:type="PerspectiveHierarchy">  
                                 <HierarchyID/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                                                            </Hierarchy>  
                           </Hierarchies>  
                           <Annotations>  
                                                            <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Dimension>  
                     </Dimensions>  
                     <MeasureGroups>  
                        <MeasureGroup xsi:type="PerspectiveMeasureGroup">  
                           <MeasureGroupID/>  
                           <Measures>  
                              <Measure xsi:type="PerspectiveMeasure">  
                                 <MeasureID/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                       <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </Measure>  
                           </Measures>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </MeasureGroup>  
                     </MeasureGroups>  
                     <Calculations>  
                        <Calculation xsi:type="PerspectiveCalculation">  
                           <Name/>  
                           <Type/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Calculation>  
                     </Calculations>  
                     <Kpis>  
                        <Kpi xsi:type="PerspectiveKpi">  
                           <KpiID/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Kpi>  
                     </Kpis>  
                     <Actions>  
                        <Action xsi:type="PerspectiveAction">  
                           <ActionID/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Action>  
                     </Actions>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                  </Perspective>  
               </Perspectives>  
               <State/>  
               <DefaultMeasure/>  
               <Visible/>  
               <MeasureGroups>  
                  <MeasureGroup>  
                     <Name/>  
                     <ID/>  
                     <CreatedTimestamp/>  
                     <LastSchemaUpdate/>  
                     <Description/>  
                     <LastProcessed/>  
                     <Type/>  
                     <State/>  
                     <Measures>  
                        <Measure>  
                           <Name/>  
                           <ID/>  
                           <Description/>  
                           <AggregateFunction/>  
                           <DataType/>  
                           <Source xsi:type="DataItem">  
                              <!-- DataItem -->  
                           </Source>  
                           <Visible/>  
                           <MeasureExpression/>  
                                                      <DisplayFolder/>  
                                                      <FormatString/>  
                           <BackColor/>  
                           <ForeColor/>  
                           <FontName/>  
                           <FontSize/>  
                           <FontFlags/>  
                           <Translations>  
                                                            <Translation>  
                                                                  <Language/>  
                                                                  <Caption/>  
                                 <Description/>  
                                 <DisplayFolder/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </Translation>  
                           </Translations>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Measure>  
                     </Measures>  
                     <Source xsi:type="MeasureGroupBinding">  
                        <!-- Binding -->  
                     </Source>  
                                          <StorageMode/>  
                     <StorageLocation/>  
                     <IgnoreUnrelatedDimensions/>  
                     <ProactiveCaching>  
                        <OnlineMode/>  
                        <AggregationStorage/>  
                        <Source xsi:type="ProactiveCachingBinding">  
                                                      <!-- Binding -->  
                        </Source>  
                        <SilenceInterval/>  
                        <Latency/>  
                        <SilenceOverrideInterval/>  
                        <ForceRebuildInterval/>  
                        <Enabled/>  
                     </ProactiveCaching>  
                     <EstimatedRows/>  
                                          <ErrorConfiguration>  
                                                <KeyErrorLimit/>  
                        <KeyErrorLogFile/>  
                        <KeyErrorAction/>  
                        <KeyErrorLimitAction/>  
                        <KeyNotFound/>  
                        <KeyDuplicate/>  
                        <NullKeyConvertedToUnknown/>  
                        <NullKeyNotAllowed/>  
                     </ErrorConfiguration>  
                     <EstimatedSize/>  
                     <ProcessingMode/>  
                     <Dimensions>  
                                                <Dimension xsi:type="ManyToManyMeasureGroupDimension">  
                           <Source xsi:type="MeasureGroupDimensionBinding">  
                              <!-- Binding -->  
                           </Source>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                           <MeasureGroupID/>  
                        </Dimension>  
                        <Dimension xsi:type="RegularMeasureGroupDimension">  
                           <Source xsi:type="MeasureGroupDimensionBinding">  
                              <!-- Binding -->  
                           </Source>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                      <Attributes>  
                              <Attribute xsi:type="MeasureGroupAttribute">  
                                 <AttributeID/>  
                                 <KeyColumns>  
                                    <KeyColumn xsi:type="DataItem">  
                                                                              <!-- DataItem -->  
                                    </KeyColumn>  
                                 </KeyColumns>  
                                 <Type/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                       <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                                                            </Attribute>  
                           </Attributes>  
                           <Hierarchies>  
                                                            <Hierarchy xsi:type="MeasureGroupHierarchy">  
                                                                  <HierarchyID/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </Hierarchy>  
                           </Hierarchies>  
                        </Dimension>  
                        <Dimension xsi:type="ReferenceMeasureGroupDimension">  
                           <Source xsi:type="MeasureGroupDimensionBinding">  
                              <!-- Binding -->  
                           </Source>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                      <IntermediateCubeDimensionID/>  
                           <IntermediateGranularityAttributeID/>  
                                                      <GranularityAttributeID/>  
                                                </Dimension>  
                        <Dimension xsi:type="DegenerateMeasureGroupDimension">  
                                                      <Source xsi:type="MeasureGroupDimensionBinding">  
                              <!-- Binding -->  
                           </Source>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                      <Attributes>  
                              <Attribute xsi:type="MeasureGroupAttribute">  
                                 <AttributeID/>  
                                 <KeyColumns>  
                                    <KeyColumn xsi:type="DataItem">  
                                                                              <!-- DataItem -->  
                                    </KeyColumn>  
                                 </KeyColumns>  
                                 <Type/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                       <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                                                            </Attribute>  
                           </Attributes>  
                           <Hierarchies>  
                                                            <Hierarchy xsi:type="MeasureGroupHierarchy">  
                                                                  <HierarchyID/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                                                            </Hierarchy>  
                           </Hierarchies>  
                        </Dimension>  
                        <Dimension xsi:type="DataMiningMeasureGroupDimension">  
                                                      <Source xsi:type="MeasureGroupDimensionBinding">  
                              <!-- Binding -->  
                           </Source>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                      <CaseCubeDimensionID/>  
                        </Dimension>  
                     <Dimensions>  
                     <Partitions>  
                                                <Partition>  
                           <Name/>  
                           <ID/>  
                           <CreatedTimestamp/>  
                           <LastSchemaUpdate/>  
                           <Description/>  
                           <Source xsi:type="TabularBinding">  
                              <!-- Binding -->  
                                                      </Source>  
                           <AggregationPrefix/>  
                                                      <StorageMode/>  
                                                      <ProcessingMode/>  
                                                      <ErrorConfiguration>  
                                                            <KeyErrorLimit/>  
                              <KeyErrorLogFile/>  
                              <KeyErrorAction/>  
                              <KeyErrorLimitAction/>  
                              <KeyNotFound/>  
                              <KeyDuplicate/>  
                              <NullKeyConvertedToUnknown/>  
                              <NullKeyNotAllowed/>  
                           </ErrorConfiguration>  
                           <StorageLocation/>  
                           <RemoteDatasourceID/>  
                           <Slice/>  
                           <ProactiveCaching>  
                              <OnlineMode/>  
                              <AggregationStorage/>  
                              <Source xsi:type="ProactiveCachingBinding">  
                                 <!-- Binding -->  
                              </Source>  
                              <SilenceInterval/>  
                              <Latency/>  
                              <SilenceOverrideInterval/>  
                              <ForceRebuildInterval/>  
                              <Enabled/>  
                           </ProactiveCaching>  
                           <Type/>  
                           <EstimatedSize/>  
                           <EstimatedRows/>  
                           <CurrentStorageMode/>  
                           <AggregationDesignID/>  
                           <LastProcessed/>  
                           <State/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                                                </Partition>  
                     </Partitions>  
                     <AggregationPrefix/>  
                                          <AggregationDesigns>  
                        <AggregationDesign>  
                           <ID/>  
                           <Name/>  
                           <Description/>  
                           <EstimatedRows/>  
                           <Dimensions>  
                              <Dimension xsi:type="AggregationDesignDimension">  
                                 <CubeDimensionID/>  
                                 <Attributes>  
                                    <Attribute xsi:type="AggregationDesignAttribute">  
                                       <AttributeID/>  
                                       <EstimatedCount/>  
                                    </Attribute>  
                                 </Attributes>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                                                              <Visibility/>  
                                       <Value/>  
                                       <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                                                            </Dimension>  
                           </Dimensions>  
                           <Aggregations>  
                                                            <Aggregation>  
                                 <ID/>  
                                                                  <Name/>  
                                 <Description/>  
                                 <Dimensions>  
                                    <Dimension xsi:type="AggregationDimension">  
                                       <CubeDimensionID/>  
                                       <Attributes>  
                                          <Attribute xsi:type="AggregationAttribute">  
                                             <AttributeID/>  
                                             <Annotations>  
                                                <Annotation>  
                                                   <Name/>  
                                                   <Visibility/>  
                                                   <Value/>  
                                                                                                      <!-- User-defined elements -->  
                                                </Annotation>  
                                             </Annotations>  
                                          </Attribute>  
                                       </Attributes>  
                                       <Annotations>  
                                          <Annotation>  
                                             <Name/>  
                                             <Visibility/>  
                                             <Value/>  
                                             <!-- User-defined elements -->  
                                          </Annotation>  
                                       </Annotations>  
                                    </Dimension>  
                                 </Dimensions  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                       <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </Aggregation>  
                           </Aggregations>  
                           <EstimatedPerformanceGain/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                 <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </AggregationDesign>  
                     </AggregationDesign>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                  </MeasureGroup>  
               </MeasureGroups>  
               <Source xsi:type="DataSourceViewBinding">  
                  <!-- Binding -->  
               </Source>  
                              <AggregationPrefix/>  
               <StorageMode/>  
                              <ProcessingMode/>  
               <ScriptCacheProcessingMode/>  
               <ProactiveCaching>  
                  <OnlineMode/>  
                  <AggregationStorage/>  
                  <Source xsi:type="ProactiveCachingBinding">  
                     <!-- Binding -->  
                  </Source>  
                  <SilenceInterval/>  
                  <Latency/>  
                  <SilenceOverrideInterval/>  
                  <ForceRebuildInterval/>  
                  <Enabled/>  
               </ProactiveCaching>  
               <Kpis>  
                  <Kpi>  
                     <Name/>  
                     <ID/>  
                     <Description/>  
                     <Translations>  
                                                <Translation>  
                                                    <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                         </Translation>  
                      </Translations>  
                     <DisplayFolder/>  
                     <AssociatedMeasureGroupID/>  
                     <Value/>  
                                          <Goal/>  
                     <Status/>  
                                          <Trend/>  
                     <TrendGraphic/>  
                                          <StatusGraphic/>  
                     <CurrentTimeMember/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                                    </Kpi>  
               </Kpis>  
               <ErrorConfiguration>  
                                    <KeyErrorLimit/>  
                  <KeyErrorLogFile/>  
                  <KeyErrorAction/>  
                  <KeyErrorLimitAction/>  
                  <KeyNotFound/>  
                  <KeyDuplicate/>  
                  <NullKeyConvertedToUnknown/>  
                  <NullKeyNotAllowed/>  
               </ErrorConfiguration>  
               <Actions>  
                  <Action xsi:type="StandardAction">  
                     <Name/>  
                     <ID/>  
                                          <Caption/>  
                     <CaptionIsMdx/>  
                                          <Translations>  
                                                <Translation>  
                                                    <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Translation>  
                     </Translations>  
                     <TargetType/>  
                     <Target/>  
                     <Condition/>  
                     <Type/>  
                     <Invocation/>  
                     <Application/>  
                     <Description/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                     <Expression/>  
                  </Action>  
                  <Action xsi:type="ReportAction">  
                     <Name/>  
                     <ID/>  
                     <Caption/>  
                                          <CaptionIsMdx/>  
                     <Translations>  
                        <Translation>  
                                                    <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Translation>  
                     </Translations>  
                     <TargetType/>  
                     <Target/>  
                     <Condition/>  
                     <Type/>  
                     <Invocation/>  
                     <Application/>  
                     <Description/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                     <ReportServer/>  
                     <Path/>  
                     <ReportParameters>  
                        <ReportParameter>  
                           <Name/>  
                           <Value/>  
                        </ReportParameter>  
                     </ReportParameters>  
                     <ReportFormatParameters>  
                        <ReportFormatParameter>  
                           <Name/>  
                           <Value/>  
                        </ReportFormatParameter>  
                     </ReportFormatParameters>  
                  </Action>  
                  <Action xsi:type="DrillThroughAction">  
                     <Name/>  
                     <ID/>  
                     <Caption/>  
                                          <CaptionIsMdx/>  
                     <Translations>  
                        <Translation>  
                                                    <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Translation>  
                     </Translations>  
                     <TargetType/>  
                     <Target/>  
                     <Condition/>  
                     <Type/>  
                     <Invocation/>  
                     <Application/>  
                     <Description/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                                          <Default/>  
                                          <Columns>  
                        <Column>  
                                                      <!-- Binding -->  
                        </Column>  
                     </Columns>  
                  </Action>  
               </Actions>  
               <StorageLocation/>  
               <EstimatedRows/>  
               <LastProcessed/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
            </Cube>  
         </Cubes>  
         <MiningStructures>  
            <MiningStructure>  
               <Name/>  
               <ID/>  
               <Description/>  
               <Source>  
                  <!-- Binding -->  
               </Source>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <LastProcessed/>  
               <Translations>  
                                    <Translation>  
                                        <Language/>  
                                          <Caption/>  
                     <Description/>  
                     <DisplayFolder/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                   </Translation>  
               </Translations>  
               <Language/>  
               <Collation/>  
                              <ErrorConfiguration>  
                                    <KeyErrorLimit/>  
                  <KeyErrorLogFile/>  
                  <KeyErrorAction/>  
                  <KeyErrorLimitAction/>  
                  <KeyNotFound/>  
                  <KeyDuplicate/>  
                  <NullKeyConvertedToUnknown/>  
                  <NullKeyNotAllowed/>  
               </ErrorConfiguration>  
               <Columns>  
                  <Column xsi:type="ScalarMiningStructureColumn">  
                     <Name/>  
                     <ID/>  
                     <Description/>  
                     <Type/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                     <IsKey/>  
                     <Source>  
                        <!-- Binding -->  
                     </Source>  
                     <Distribution/>  
                     <ModelingFlags>  
                        <ModelingFlag xsi:type="MiningModelingFlag"/>  
                     </ModelingFlags>  
                     <Content/>  
                     <ClassifiedColumns>  
                        <ClassifiedColumnID/>  
                     </ClassifiedColumns>  
                     <DiscretizationMethod/>  
                     <DiscretizationBucketCount/>  
                     <KeyColumns>  
                        <KeyColumn xsi:type="DataItem">  
                           <!-- DataItem -->  
                        </KeyColumn>  
                     </KeyColumns>  
                     <NameColumn/>  
                     <Translations>  
                                                <Translation>  
                                                    <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Translation>  
                     </Translations>  
                  </Column>  
                  <Column xsi:type="TableMiningStructureColumn">  
                     <Name/>  
                     <ID/>  
                     <Description/>  
                     <Type/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                           <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                     <ForeignKeyColumns>  
                        <ForeignKeyColumn xsi:type="DataItem">  
                           <!-- DataItem -->  
                        </ForeignKeyColumn>  
                     </ForeignKeyColumns>  
                     <SourceMeasureGroup xsi:type="MeasureGroupBinding">  
                        <!-- Binding -->  
                     </SourceMeasureGroup>  
                     <Columns>  
                        <!-- for nested tables -->  
                     </Columns>  
                     <Translations>  
                                                <Translation>  
                                                    <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Translation>  
                     </Translations>  
                  </Column>  
               </Columns>  
               <State/>  
               <MiningStructurePermissions>  
                  <MiningStructurePermission>  
                     <Name/>  
                     <ID/>  
                     <CreatedTimestamp/>  
                                          <LastSchemaUpdate/>  
                     <RoleID/>  
                     <Description/>  
                                          <Process/>  
                     <ReadDefinition/>  
                                          <Access/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                  </MiningStructurePermission>  
               </MiningStructurePermission>  
               <MiningModels>  
                  <MiningModel>  
                     <Name/>  
                     <ID/>  
                     <Description/>  
                     <Algorithm/>  
                     <CreatedTimestamp/>  
                     <LastSchemaUpdate/>  
                     <LastProcessed/>  
                     <AlgorithmParameters>  
                        <AlgorithmParameter>  
                           <Name/>  
                           <Value/>  
                        </AlgorithmParameter>  
                     </AlgorithmParameters>  
                     <AllowDrillThrough/>  
                     <Translations>  
                                                <Translation>  
                                                    <Language/>  
                                                      <Caption/>  
                           <Description/>  
                           <DisplayFolder/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Translation>  
                     </Translations>  
                     <Columns>  
                        <Column xsi:type="MiningModelColumn">  
                           <Name/>  
                           <ID/>  
                           <Description/>  
                           <SourceColumnID/>  
                                                      <Usage/>  
                                                      <Translations>  
                                                            <Translation>  
                                                                  <Language/>  
                                                                  <Caption/>  
                                 <Description/>  
                                 <DisplayFolder/>  
                                 <Annotations>  
                                    <Annotation>  
                                       <Name/>  
                                       <Visibility/>  
                                       <Value/>  
                                                                              <!-- User-defined elements -->  
                                    </Annotation>  
                                 </Annotations>  
                              </Translation>  
                           </Translations>  
                           <Columns>  
                              <!-- for nested tables -->  
                                                      </Columns>  
                           <ModelingFlags>  
                              <ModelingFlag xsi:type="MiningModelingFlag"/>  
                           </ModelingFlags>  
                            <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                        </Column>  
                     </Columns>  
                     <State/>  
                                          <MiningModelPermissions>  
                        <MiningModelPermission>  
                                                      <Name/>  
                           <ID/>  
                           <CreatedTimestamp/>  
                                                      <LastSchemaUpdate/>  
                           <RoleID/>  
                           <Description/>  
                                                      <Process/>  
                           <ReadDefinition/>  
                                                      <Access/>  
                           <Annotations>  
                              <Annotation>  
                                 <Name/>  
                                 <Visibility/>  
                                 <Value/>  
                                                                  <!-- User-defined elements -->  
                              </Annotation>  
                           </Annotations>  
                           <AllowDrillThrough/>  
                           <AllowBrowsing/>  
                        </MiningModelPermission>  
                     </MiningModelPermissions>  
                     <Language/>  
                                          <Collation/>  
                     <Annotations>  
                        <Annotation>  
                           <Name/>  
                           <Visibility/>  
                           <Value/>  
                                                      <!-- User-defined elements -->  
                        </Annotation>  
                     </Annotations>  
                  </MiningModel>  
               </MiningModels>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
            </MiningStructure>  
         </MiningStructures>  
         <MasterDatasourceID/>  
         <Assemblies>  
            <Assembly xsi:type="ClrAssembly">  
               <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <Description/>  
               <ImpersonationMode/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
               <Files>  
                  <File xsi:type="ClrAssemblyFile">  
                     <Name/>  
                     <Type/>  
                     <Data>  
                        <Blocks>  
                                                      <Block xsi:type="DataBlock"/>  
                        </Blocks>  
                     </Data>  
                  </File>  
               </Files>  
               <PermissionSet/>  
            </Assembly>  
            <Assembly xsi:type="ComAssembly">  
               <Name/>  
               <ID/>  
                              <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <Description/>  
               <ImpersonateMode/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
               <Source/>  
            </Assembly>  
         </Assemblies>  
         <DataSources>  
            <DataSource xsi:type="RelationalDataSource">  
               <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <ConnectionString/>  
               <ConnectionStringSecurity/>  
               <Description/>  
               <Timeout/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
            </DataSource>  
            <DataSource xsi:type="OlapDataSource">  
               <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <ConnectionString/>  
               <ConnectionStringSecurity/>  
               <Description/>  
               <Timeout/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
            </DataSource>  
         </DataSources>  
         <DataSourceViews>  
            <DataSourceView>  
               <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <Description/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
                              <DataSourceID/>  
               <Schema/>  
                        </DataSourceView>  
         </DataSourceViews>  
         <Accounts>  
            <Account>  
               <AccountType/>  
               <AggregationFunction/>  
               <Aliases>  
                                    <Alias>  
                  </Alias>  
               </Alias>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                                          <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
            </Account>  
         </Accounts>  
         <Roles>  
            <Role>  
               <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
               <LastSchemaUpdate/>  
               <Description/>  
               <Members>  
                  <Member/>  
               </Members>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                     <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
                        </Role>  
         </Role>  
         <DatabasePermissions>  
            <DatabasePermission>  
                              <Name/>  
               <ID/>  
               <CreatedTimestamp/>  
                              <LastSchemaUpdate/>  
               <RoleID/>  
               <Description/>  
                              <Process/>  
               <ReadDefinition/>  
                              <Access/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                                          <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
               <Administer/>  
            </DatabasePermission>  
         </DatabasePermissions>  
         <Translations>  
            <Translation>  
               <Language/>  
                              <Caption/>  
               <Description/>  
               <DisplayFolder/>  
               <Annotations>  
                  <Annotation>  
                     <Name/>  
                     <Visibility/>  
                     <Value/>  
                                          <!-- User-defined elements -->  
                  </Annotation>  
               </Annotations>  
            </Translation>  
         </Translations>  
         <Annotations>  
            <Annotation>  
               <Name/>  
               <Visibility/>  
               <Value/>  
               <!-- User-defined elements -->  
            </Annotation>  
         </Annotations>  
      </Database>  
   </Databases>  
   <Assemblies>  
      <Assembly xsi:type="ClrAssembly">  
         <Name/>  
         <ID/>  
         <CreatedTimestamp/>  
         <LastSchemaUpdate/>  
         <Description/>  
         <ImpersonateMode/>  
         <Annotations>  
            <Annotation>  
               <Name/>  
               <Visibility/>  
               <Value/>  
               <!-- User-defined elements -->  
            </Annotation>  
         </Annotations>  
         <Files>  
            <File xsi:type="ClrAssemblyFile">  
               <Name/>  
               <Type/>  
               <Data>  
                  <Blocks>  
                                          <Block xsi:type="DataBlock"/>  
                  </Blocks>  
               </Data>  
            </File>  
         </Files>  
         <PermissionSet/>  
      </Assembly>  
      <Assembly xsi:type="ComAssembly">  
         <Name/>  
         <ID/>  
                  <CreatedTimestamp/>  
         <LastSchemaUpdate/>  
         <Description/>  
         <ImpersonateMode/>  
         <Annotations>  
            <Annotation>  
               <Name/>  
               <Visibility/>  
               <Value/>  
               <!-- User-defined elements -->  
            </Annotation>  
         </Annotations>  
         <Source/>  
      </Assembly>  
   </Assemblies>  
   <Traces>  
      <Trace>  
         <Name/>  
         <ID/>  
         <CreatedTimestamp/>  
         <LastSchemaUpdate/>  
         <Description/>  
         <LogFileName/>  
         <LogFileAppend/>  
         <LogFileSize/>  
                  <Audit/>  
         <LogFileRollover/>  
         <AutoRestart/>  
         <StopTime/>  
         <Filter/>  
                  <Events>  
                        <Event>  
                              <EventID/>  
               <Columns>  
                  <Column xsi:type="EventColumn">  
                     <ColumnID/>  
                                    </Column>  
               </Column>  
            </Event>  
         </Events>  
         <Annotations>  
            <Annotation>  
               <Name/>  
               <Visibility/>  
               <Value/>  
               <!-- User-defined elements -->  
            </Annotation>  
         </Annotations>  
            </Trace>  
   </Traces>  
   <Roles>  
            <Role>  
                  <Name/>  
         <ID/>  
         <CreatedTimestamp/>  
         <LastSchemaUpdate/>  
         <Description/>  
         <Members>  
            <Member/>  
         </Members>  
         <Annotations>  
            <Annotation>  
               <Name/>  
               <Visibility/>  
               <Value/>  
               <!-- User-defined elements -->  
            </Annotation>  
         </Annotations>  
      </Role>  
   </Roles>  
   <ServerProperties>  
      <ServerProperty>  
         <Name/>  
         <Value/>  
         <RequiresRestart/>  
         <PendingValue/>  
         <DefaultValue/>  
         <DisplayFlag/>  
      </ServerProperty>  
   </ServerProperties>  
   <ProductName/>  
   <Annotations>  
      <Annotation>  
         <Name/>  
         <Visibility/>  
         <Value/>  
         <!-- User-defined elements -->  
      </Annotation>  
   </Annotations>  
</Server>  
```  
  
## See Also  
 [Analysis Services Scripting Language XML Elements &#40;ASSL&#41;](../../analysis-services/scripting/analysis-services-scripting-language-xml-elements-assl.md)   
 [Analysis Services Scripting Language XML Data Type Hierarchy &#40;ASSL&#41;](../../analysis-services/scripting/analysis-services-scripting-language-xml-data-type-hierarchy-assl.md)   
 [Analysis Services Scripting Language &#40;ASSL for XMLA&#41;](../../analysis-services/scripting/analysis-services-scripting-language-assl-for-xmla.md)  
  
  