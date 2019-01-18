---
title: "MDX Function Reference (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MDX Function Reference (MDX)


  Analysis Services provides for the use of functions in Multidimensional Expressions (MDX) syntax. Functions can be used in any valid MDX statement, and are frequently used in queries, custom rollup definitions, and other calculations. This section provides information about the MDX functions.  
  
 You can use the following tables to find functions by their category of return value, or you can select a function by name from the alphabetical list in the table of contents.  
  
## Array Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[SetToArray &#40;MDX&#41;](../mdx/settoarray-mdx.md)|Converts one or more sets to an array for use in a user-defined function.|  
  
## Hierarchy Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[Hierarchy &#40;MDX&#41;](../mdx/hierarchy-mdx.md)|Returns the hierarchy that contains a specified member or level.|  
|[Dimension &#40;MDX&#41;](../mdx/dimension-mdx.md)|Returns the dimension that contains a specified member, level, or hierarchy.|  
|[Dimensions &#40;MDX&#41;](../mdx/dimensions-mdx.md)|Returns a hierarchy specified by a numeric or string expression.|  
  
## Level Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[Level &#40;MDX&#41;](../mdx/level-mdx.md)|Returns the level of a member.|  
|[Levels &#40;MDX&#41;](../mdx/levels-mdx.md)|Returns the level whose position in a dimension or hierarchy is specified by a numeric expression or whose name is specified by a string expression.|  
  
## Logical Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[IsAncestor &#40;MDX&#41;](../mdx/isancestor-mdx.md)|Returns whether a specified member is an ancestor of another specified member.|  
|[IsEmpty &#40;MDX&#41;](../mdx/isempty-mdx.md)|Returns whether the evaluated expression is the empty cell value.|  
|[IsGeneration &#40;MDX&#41;](../mdx/isgeneration-mdx.md)|Returns whether a specified member is in a specified generation.|  
|[IsLeaf &#40;MDX&#41;](../mdx/isleaf-mdx.md)|Returns whether a specified member is a leaf member.|  
|[IsSibling &#40;MDX&#41;](../mdx/issibling-mdx.md)|Returns whether a specified member is a sibling of another specified member.|  
  
## Member Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[Ancestor &#40;MDX&#41;](../mdx/ancestor-mdx.md)|Returns the ancestor of a member at a specified level or distance.|  
|[ClosingPeriod &#40;MDX&#41;](../mdx/closingperiod-mdx.md)|Returns the last sibling among the descendants of a member at a specified level.|  
|[Cousin &#40;MDX&#41;](../mdx/cousin-mdx.md)|Returns the child member with the same relative position under a parent member as the specified child member.|  
|[CurrentMember &#40;MDX&#41;](../mdx/currentmember-mdx.md)|Returns the current member along a specified dimension or hierarchy during iteration.|  
|[DataMember &#40;MDX&#41;](../mdx/datamember-mdx.md)|Returns the system-generated data member that is associated with a nonleaf member of a dimension.|  
|[DefaultMember &#40;MDX&#41;](../mdx/defaultmember-mdx.md)|Returns the default member of a dimension or hierarchy.|  
|[FirstChild &#40;MDX&#41;](../mdx/firstchild-mdx.md)|Returns the first child of a member.|  
|[FirstSibling &#40;MDX&#41;](../mdx/firstsibling-mdx.md)|Returns the first child of the parent of a member.|  
|[Item &#40;Member&#41; &#40;MDX&#41;](../mdx/item-member-mdx.md)|Returns a member from a specified tuple.|  
|[Lag &#40;MDX&#41;](../mdx/lag-mdx.md)|Returns the member that is a specified number of positions before a specified member along the member's dimension.|  
|[LastChild &#40;MDX&#41;](../mdx/lastchild-mdx.md)|Returns the last child of a specified member.|  
|[LastSibling &#40;MDX&#41;](../mdx/lastsibling-mdx.md)|Returns the last child of the parent of a specified member.|  
|[Lead &#40;MDX&#41;](../mdx/lead-mdx.md)|Returns the member that is a specified number of positions following a specified member along the member's dimension.|  
|[LinkMember &#40;MDX&#41;](../mdx/linkmember-mdx.md)|Returns the member equivalent to a specified member in a specified hierarchy.|  
|[Members &#40;String&#41; &#40;MDX&#41;](../mdx/members-string-mdx.md)|Returns a member specified by a string expression.|  
|[NextMember &#40;MDX&#41;](../mdx/nextmember-mdx.md)|Returns the next member in the level that contains a specified member.|  
|[OpeningPeriod &#40;MDX&#41;](../mdx/openingperiod-mdx.md)|Returns the first sibling among the descendants of a specified level, optionally at a specified member.|  
|[ParallelPeriod &#40;MDX&#41;](../mdx/parallelperiod-mdx.md)|Returns a member from a prior period in the same relative position as a specified member.|  
|[Parent &#40;MDX&#41;](../mdx/parent-mdx.md)|Returns the parent of a member.|  
|[PrevMember &#40;MDX&#41;](../mdx/prevmember-mdx.md)|Returns the previous member in the level that contains a specified member.|  
|[StrToMember &#40;MDX&#41;](../mdx/strtomember-mdx.md)|Returns the member specified by an MDX-formatted string.|  
|[UnknownMember &#40;MDX&#41;](../mdx/unknownmember-mdx.md)|Returns the unknown member associated with a level or member.|  
|[ValidMeasure &#40;MDX&#41;](../mdx/validmeasure-mdx.md)|Returns a valid measure in a virtual cube by forcing inapplicable dimensions to their top level.|  
  
## Numeric Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[Aggregate &#40;MDX&#41;](../mdx/aggregate-mdx.md)|Returns a scalar value calculated by aggregating either measures or an optionally specified numeric expression over the tuples of a specified set.|  
|[Avg &#40;MDX&#41;](../mdx/avg-mdx.md)|Returns the average value of measures or the average value of an optional numeric expression, evaluated over a specified set.|  
|[CalculationCurrentPass &#40;MDX&#41;](../mdx/calculationcurrentpass-mdx.md)|Returns the current calculation pass of a cube for the specified query context.|  
|[CalculationPassValue &#40;MDX&#41;](../mdx/calculationpassvalue-mdx.md)|Returns the value of a MDX expression evaluated over the specified calculation pass of a cube.|  
|[CoalesceEmpty &#40;MDX&#41;](../mdx/coalesceempty-mdx.md)|Coalesces an empty cell value to a number or string and returns the coalesced value.|  
|[Correlation &#40;MDX&#41;](../mdx/correlation-mdx.md)|Returns the correlation coefficient of two series evaluated over a set.|  
|[Count &#40;Dimension&#41; &#40;MDX&#41;](../mdx/count-dimension-mdx.md)|Returns the number of dimensions in a cube.|  
|[Count &#40;Hierarchy Levels&#41; &#40;MDX&#41;](../mdx/count-hierarchy-levels-mdx.md)|Returns the number of levels in a dimension or hierarchy.|  
|[Count &#40;Set&#41; &#40;MDX&#41;](../mdx/count-set-mdx.md)|Returns the number of cells in a set.|  
|[Count &#40;Tuple&#41; &#40;MDX&#41;](../mdx/count-tuple-mdx.md)|Returns the number of dimensions in a tuple.|  
|[Covariance &#40;MDX&#41;](../mdx/covariance-mdx.md)|Returns the population covariance of two series evaluated over a set, using the biased population formula.|  
|[CovarianceN &#40;MDX&#41;](../mdx/covariancen-mdx.md)|Returns the sample covariance of two series evaluated over a set, using the unbiased population formula.|  
|[DistinctCount &#40;MDX&#41;](../mdx/distinctcount-mdx.md)|Returns the number of distinct, nonempty tuples in a set.|  
|[IIf &#40;MDX&#41;](../mdx/iif-mdx.md)|Returns one of two values determined by a logical test.|  
|[LinRegIntercept &#40;MDX&#41;](../mdx/linregintercept-mdx.md)|Calculates the linear regression of a set and returns the value of the intercept in the regression line, y = ax + b.|  
|[LinRegPoint &#40;MDX&#41;](../mdx/linregpoint-mdx.md)|Calculates the linear regression of a set and returns the value of *y* in the regression line, y = ax + b.|  
|[LinRegR2 &#40;MDX&#41;](../mdx/linregr2-mdx.md)|Calculates the linear regression of a set and returns the coefficient of determination, R2.|  
|[LinRegSlope &#40;MDX&#41;](../mdx/linregslope-mdx.md)|Calculates the linear regression of a set, and returns the value of the slope in the regression line, y = ax + b.|  
|[LinRegVariance &#40;MDX&#41;](../mdx/linregvariance-mdx.md)|Calculates the linear regression of a set, and returns the variance associated with the regression line, y = ax + b.|  
|[LookupCube &#40;MDX&#41;](../mdx/lookupcube-mdx.md)|Returns the value of an MDX expression evaluated over another specified cube in the same database.|  
|[Max &#40;MDX&#41;](../mdx/max-mdx.md)|Returns the maximum value of a numeric expression that is evaluated over a set.|  
|[Median &#40;MDX&#41;](../mdx/median-mdx.md)|Returns the median value of a numeric expression that is evaluated over a set.|  
|[Min &#40;MDX&#41;](../mdx/min-mdx.md)|Returns the minimum value of a numeric expression that is evaluated over a set.|  
|[Ordinal &#40;MDX&#41;](../mdx/ordinal-mdx.md)|Returns the zero-based ordinal value associated with a level.|  
|[Predict &#40;MDX&#41;](../mdx/predict-mdx.md)|Returns a value of a numeric expression evaluated over a data mining model.|  
|[Rank &#40;MDX&#41;](../mdx/rank-mdx.md)|Returns the one-based rank of a specified tuple in a specified set.|  
|[RollupChildren &#40;MDX&#41;](../mdx/rollupchildren-mdx.md)|Returns a value generated by rolling up the values of the children of a specified member using the specified unary operator.|  
|[Stddev &#40;MDX&#41;](../mdx/stddev-mdx.md)|Alias for [Stdev &#40;MDX&#41;](../mdx/stdev-mdx.md).|  
|[StddevP &#40;MDX&#41;](../mdx/stddevp-mdx.md)|Alias for [StdevP &#40;MDX&#41;](../mdx/stdevp-mdx.md).|  
|[Stdev &#40;MDX&#41;](../mdx/stdev-mdx.md)|Returns the sample standard deviation of a numeric expression evaluated over a set, using the unbiased population formula.|  
|[StdevP &#40;MDX&#41;](../mdx/stdevp-mdx.md)|Returns the population standard deviation of a numeric expression evaluated over a set, using the biased population formula.|  
|[StrToValue &#40;MDX&#41;](../mdx/strtovalue-mdx.md)|Returns the value specified by an MDX-formatted string.|  
|[Sum &#40;MDX&#41;](../mdx/sum-mdx.md)|Returns the sum of a numeric expression evaluated over a set.|  
|[Value &#40;MDX&#41;](../mdx/value-mdx.md)|Returns the value of a measure.|  
|[Var &#40;MDX&#41;](../mdx/var-mdx.md)|Returns the sample variance of a numeric expression evaluated over a set, using the unbiased population formula.|  
|[Variance &#40;MDX&#41;](../mdx/variance-mdx.md)|Alias for [Var &#40;MDX&#41;](../mdx/var-mdx.md).|  
|[VarianceP &#40;MDX&#41;](../mdx/variancep-mdx.md)|Alias for [VarP &#40;MDX&#41;](../mdx/varp-mdx.md).|  
|[VarP &#40;MDX&#41;](../mdx/varp-mdx.md)|Returns the population variance of a numeric expression evaluated over a set, using the biased population formula.|  
  
## Set Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[AddCalculatedMembers &#40;MDX&#41;](../mdx/addcalculatedmembers-mdx.md)|Returns a set generated by adding calculated members to a specified set.|  
|[AllMembers &#40;MDX&#41;](../mdx/allmembers-mdx.md)|Returns a set that contains all members, including calculated members, of the specified dimension, hierarchy, or level.|  
|[Ancestors &#40;MDX&#41;](../mdx/ancestors-mdx.md)|Returns a set of all ancestors of a member at a specified level or distance.|  
|[Ascendants &#40;MDX&#41;](../mdx/ascendants-mdx.md)|Returns the set of the ascendants of a specified member, including the member itself.|  
|[Axis &#40;MDX&#41;](../mdx/axis-mdx.md)|Returns a set defined in an axis.|  
|[BottomCount &#40;MDX&#41;](../mdx/bottomcount-mdx.md)|Sorts a set in ascending order, and returns the specified number of tuples with the lowest values.|  
|[BottomPercent &#40;MDX&#41;](../mdx/bottompercent-mdx.md)|Sorts a set in ascending order, and returns a set of tuples with the lowest values whose cumulative total is equal to or less than a specified percentage.|  
|[BottomSum &#40;MDX&#41;](../mdx/bottomsum-mdx.md)|Sorts a set in ascending order, and returns a set of tuples with the lowest values whose total is equal to or less than a specified value.|  
|[Children &#40;MDX&#41;](../mdx/children-mdx.md)|Returns the children of a specified member.|  
|[Crossjoin &#40;MDX&#41;](../mdx/crossjoin-mdx.md)|Returns the cross product of one or more sets.|  
|[CurrentOrdinal &#40;MDX&#41;](../mdx/currentordinal-mdx.md)|Returns the current iteration number within a set during iteration.|  
|[Descendants &#40;MDX&#41;](../mdx/descendants-mdx.md)|Returns the set of descendants of a member at a specified level or distance, optionally including or excluding descendants in other levels.|  
|[Distinct &#40;MDX&#41;](../mdx/distinct-mdx.md)|Returns a set, removing duplicate tuples from a specified set.|  
|[DrilldownLevel &#40;MDX&#41;](../mdx/drilldownlevel-mdx.md)|Drills down the members of a set to one level below the lowest level represented in the set, or to one level below an optionally specified level of a member represented in the set.|  
|[DrilldownLevelBottom &#40;MDX&#41;](../mdx/drilldownlevelbottom-mdx.md)|Drills down the bottommost members of a set, at a specified level, to one level below.|  
|[DrilldownLevelTop &#40;MDX&#41;](../mdx/drilldownleveltop-mdx.md)|Drills down the topmost members of a set, at a specified level, to one level below.|  
|[DrilldownMember &#40;MDX&#41;](../mdx/drilldownmember-mdx.md)|Drills down the members in a specified set that are present in a second specified set. Alternatively, the function drills down on a set of tuples.|  
|[DrilldownMemberBottom &#40;MDX&#41;](../mdx/drilldownmemberbottom-mdx.md)|Drills down the members in a specified set that are present in a second specified set, limiting the result set to a specified number of members. Alternatively, this function also drills down on a set of tuples.|  
|[DrilldownMemberTop &#40;MDX&#41;](../mdx/drilldownmembertop-mdx.md)|Drills down the members in a specified set that are present in a second specified set, limiting the result set to a specified number of members. Alternatively, this function drills down on a set of tuples.|  
|[DrillupLevel &#40;MDX&#41;](../mdx/drilluplevel-mdx.md)|Drills up the members of a set that are below a specified level.|  
|[DrillupMember &#40;MDX&#41;](../mdx/drillupmember-mdx.md)|Drills up the members in a specified set that are present in a second specified set.|  
|[Except &#40;MDX&#41;](../mdx/except-mdx-function.md)|Finds the difference between two sets, optionally retaining duplicates.|  
|[Exists &#40;MDX&#41;](../mdx/exists-mdx.md)|Returns the set of members of one set that exist with one or more tuples of one or more other sets.|  
|[Extract &#40;MDX&#41;](../mdx/extract-mdx.md)|Returns a set of tuples from extracted dimension elements.|  
|[Filter &#40;MDX&#41;](../mdx/filter-mdx.md)|Returns the set that results from filtering a specified set based on a search condition.|  
|[Generate &#40;MDX&#41;](../mdx/generate-mdx.md)|Applies a set to each member of another set, and then joins the resulting sets by union. Alternatively, this function returns a concatenated string created by evaluating a string expression over a set.|  
|[Head &#40;MDX&#41;](../mdx/head-mdx.md)|Returns the first specified number of elements in a set, while retaining duplicates.|  
|[Hierarchize &#40;MDX&#41;](../mdx/hierarchize-mdx.md)|Orders the members of a set in a hierarchy.|  
|[Intersect &#40;MDX&#41;](../mdx/intersect-mdx.md)|Returns the intersection of two input sets, optionally retaining duplicates.|  
|[LastPeriods &#40;MDX&#41;](../mdx/lastperiods-mdx.md)|Returns a set of members up to and including a specified member.|  
|[Members &#40;Set&#41; &#40;MDX&#41;](../mdx/members-set-mdx.md)|Returns the set of members in a dimension, level, or hierarchy.|  
|[Mtd &#40;MDX&#41;](../mdx/mtd-mdx.md)|Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the Year level in the Time dimension.|  
|[NameToSet &#40;MDX&#41;](../mdx/nametoset-mdx.md)|Returns a set that contains the member specified by an MDX-formatted string.|  
|[NonEmptyCrossjoin &#40;MDX&#41;](../mdx/nonemptycrossjoin-mdx.md)|Returns the cross product of one or more sets as a set, excluding empty tuples and tuples without associated fact table data.|  
|[Order &#40;MDX&#41;](../mdx/order-mdx.md)|Arranges members of a specified set, optionally preserving or breaking the hierarchy.|  
|[PeriodsToDate &#40;MDX&#41;](../mdx/periodstodate-mdx.md)|Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by a specified level in the Time dimension.|  
|[Qtd &#40;MDX&#41;](../mdx/qtd-mdx.md)|Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the *Quarter* level in the Time dimension.|  
|[Siblings &#40;MDX&#41;](../mdx/siblings-mdx.md)|Returns the siblings of a specified member, including the member itself.|  
|[StripCalculatedMembers &#40;MDX&#41;](../mdx/stripcalculatedmembers-mdx.md)|Returns a set generated by removing calculated members from a specified set.|  
|[StrToSet &#40;MDX&#41;](../mdx/strtoset-mdx.md)|Returns the set specified by an MDX-formatted string.|  
|[Subset &#40;MDX&#41;](../mdx/subset-mdx.md)|Returns a subset of tuples from a specified set.|  
|[Tail &#40;MDX&#41;](../mdx/tail-mdx.md)|Returns a subset from the end of a set.|  
|[ToggleDrillState &#40;MDX&#41;](../mdx/toggledrillstate-mdx.md)|Toggles the drill state of members.|  
|[TopCount &#40;MDX&#41;](../mdx/topcount-mdx.md)|Sorts a set in descending order and returns the specified number of elements with the highest values.|  
|[TopPercent &#40;MDX&#41;](../mdx/toppercent-mdx.md)|Sorts a set in descending order, and returns a set of tuples with the highest values whose cumulative total is equal to or less than a specified percentage.|  
|[TopSum &#40;MDX&#41;](../mdx/topsum-mdx.md)|Sorts a set and returns the topmost elements whose cumulative total is at least a specified value.|  
|[Union  &#40;MDX&#41;](../mdx/union-mdx.md)|Returns the union of two sets, optionally retaining duplicates.|  
|[Unorder &#40;MDX&#41;](../mdx/unorder-mdx.md)|Removes any enforced ordering from a specified set.|  
|[VisualTotals &#40;MDX&#41;](../mdx/visualtotals-mdx.md)|Returns a set generated by dynamically totaling child members in a specified set, optionally using a pattern for the name of the parent member in the resulting cellset.|  
|[Wtd &#40;MDX&#41;](../mdx/wtd-mdx.md)|Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the Week level in the Time dimension.|  
|[Ytd &#40;MDX&#41;](../mdx/ytd-mdx.md)|Returns a set of sibling members from the same level as a given member, starting with the first sibling and ending with the given member, as constrained by the *Year* level in the Time dimension.|  
  
## String Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[CalculationPassValue &#40;MDX&#41;](../mdx/calculationpassvalue-mdx.md)|Returns the value of an MDX expression evaluated over the specified calculation pass of a cube.|  
|[CoalesceEmpty &#40;MDX&#41;](../mdx/coalesceempty-mdx.md)|Coalesces an empty cell value to a number or string and returns the coalesced value.|  
|[Generate &#40;MDX&#41;](../mdx/generate-mdx.md)|Applies a set to each member of another set, and then joins the resulting sets by union. Alternatively, this function returns a concatenated string created by evaluating a string expression over a set.|  
|[IIf &#40;MDX&#41;](../mdx/iif-mdx.md)|Returns one of two values determined by a logical test.|  
|[LookupCube &#40;MDX&#41;](../mdx/lookupcube-mdx.md)|Returns the value of an MDX expression evaluated over another specified cube in the same database.|  
|[MemberToStr &#40;MDX&#41;](../mdx/membertostr-mdx.md)|Returns an MDX-formatted string that corresponds to a specified member.|  
|[Name &#40;MDX&#41;](../mdx/name-mdx.md)|Returns the name of a dimension, hierarchy, level, or member.|  
|[Properties &#40;MDX&#41;](../mdx/properties-mdx.md)|Returns a string, or a strongly-typed value, that contains a member property value.|  
|[SetToStr &#40;MDX&#41;](../mdx/settostr-mdx.md)|Returns an MDX-formatted string of that corresponds to a specified set.|  
|[TupleToStr &#40;MDX&#41;](../mdx/tupletostr-mdx.md)|Returns an MDX-formatted string that corresponds to specified tuple.|  
|[UniqueName &#40;MDX&#41;](../mdx/uniquename-mdx.md)|Returns the unique name of a specified dimension, hierarchy, level, or member.|  
|[UserName &#40;MDX&#41;](../mdx/username-mdx.md)|Returns the domain name and user name of the current connection.|  
  
## Subcube Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[This &#40;MDX&#41;](../mdx/this-mdx.md)|Returns the current subcube.|  
|[Leaves &#40;MDX&#41;](../mdx/leaves-mdx.md)|Returns the set of leaf members in the specified dimension, member, or tuple.|  
  
## Tuple Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[Current &#40;MDX&#41;](../mdx/current-mdx.md)|Returns the current tuple from a set during iteration.|  
|[Item &#40;Tuple&#41; &#40;MDX&#41;](../mdx/item-tuple-mdx.md)|Returns a tuple from a set.|  
|[Root &#40;MDX&#41;](../mdx/root-mdx.md)|Returns a tuple that consists of the **All** members from each attribute hierarchy in a cube, dimension, or tuple.|  
|[StrToTuple &#40;MDX&#41;](../mdx/strtotuple-mdx.md)|Returns the tuple specified by an MDX-formatted string.|  
  
## Other Functions  
  
|Function|Description|  
|--------------|-----------------|  
|[Error &#40;MDX&#41;](../mdx/error-mdx.md)|Raises an error, optionally providing a specified error message.|  
  
## See Also  
 [MDX Language Reference &#40;MDX&#41;](../mdx/mdx-language-reference-mdx.md)  
  
  
