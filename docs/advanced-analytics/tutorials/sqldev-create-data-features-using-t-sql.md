---
title: Lesson 2 Create data features using R and T-SQL functions - SQL Server Machine Learning
description: Tutorial showing how to add calculations to stored procedures for use in R machine learning models.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/19/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Lesson 2: Create data features using R and T-SQL
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is part of a tutorial for SQL developers on how to use R in SQL Server.

In this step, you'll learn how to create features from raw data by using a [!INCLUDE[tsql](../../includes/tsql-md.md)] function. You'll then call that function from a stored procedure to create a table that contains the feature values.

## About feature engineering

After several rounds of data exploration, you have collected some insights from the data, and are ready to move on to *feature engineering*. This process of creating meaningful features from the raw data is a critical step in creating analytical models.

In this dataset, the distance values are based on the reported meter distance, and don't necessarily represent geographical distance or the actual distance traveled. Therefore, you'll need to calculate the direct distance between the pick-up and drop-off points, by using the coordinates available in the source NYC Taxi dataset. You can do this by using the [Haversine formula](https://en.wikipedia.org/wiki/Haversine_formula) in a custom [!INCLUDE[tsql](../../includes/tsql-md.md)] function.

You'll use one custom T-SQL function, _fnCalculateDistance_, to compute the distance using the Haversine formula, and use a second custom T-SQL function, _fnEngineerFeatures_, to create a table containing all the features.

The overall process is as follows:

- Create the T-SQL function that performs the calculations

- Call the function to generate the feature data

- Save the feature data to a table

## Calculate trip distance using fnCalculateDistance

The function _fnCalculateDistance_ should have been downloaded and registered with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as part of the preparation for this tutorial. Take a minute to review the code.
  
1. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], expand **Programmability**, expand **Functions** and then **Scalar-valued functions**.   

2. Right-click _fnCalculateDistance_, and select **Modify** to open the [!INCLUDE[tsql](../../includes/tsql-md.md)] script in a new query window.
  
    ```sql
    CREATE FUNCTION [dbo].[fnCalculateDistance] (@Lat1 float, @Long1 float, @Lat2 float, @Long2 float)  
    -- User-defined function that calculates the direct distance between two geographical coordinates.  
    RETURNS float  
    AS  
    BEGIN  
      DECLARE @distance decimal(28, 10)  
      -- Convert to radians  
      SET @Lat1 = @Lat1 / 57.2958  
      SET @Long1 = @Long1 / 57.2958  
      SET @Lat2 = @Lat2 / 57.2958  
      SET @Long2 = @Long2 / 57.2958  
      -- Calculate distance  
      SET @distance = (SIN(@Lat1) * SIN(@Lat2)) + (COS(@Lat1) * COS(@Lat2) * COS(@Long2 - @Long1))  
      --Convert to miles  
      IF @distance <> 0  
      BEGIN  
        SET @distance = 3958.75 * ATAN(SQRT(1 - POWER(@distance, 2)) / @distance);  
      END  
      RETURN @distance  
    END
    GO
    ```
  
    - The function is a scalar-valued function, returning a single data value of a predefined type.
  
    - It takes latitude and longitude values as inputs, obtained from trip pick-up and drop-off locations. The Haversine formula converts locations to radians and uses those values to compute the direct distance in miles between those two locations.

## Generate the features using _fnEngineerFeatures_

To add the computed values to a table that can be used for training the model, you'll use another function, _fnEngineerFeatures_. The new function calls the previously created T-SQL function, _fnCalculateDistance_, to get the direct distance between pick-up and drop-off locations. 

1. Take a minute to review the code for the custom T-SQL function, _fnEngineerFeatures_, which should have been created for you as part of the preparation for this walkthrough.
  
    ```sql
    CREATE FUNCTION [dbo].[fnEngineerFeatures] (  
    @passenger_count int = 0,  
    @trip_distance float = 0,  
    @trip_time_in_secs int = 0,  
    @pickup_latitude float = 0,  
    @pickup_longitude float = 0,  
    @dropoff_latitude float = 0,  
    @dropoff_longitude float = 0)  
    RETURNS TABLE  
    AS
      RETURN
      (
      -- Add the SELECT statement with parameter references here
      SELECT
        @passenger_count AS passenger_count,
        @trip_distance AS trip_distance,
        @trip_time_in_secs AS trip_time_in_secs,
        [dbo].[fnCalculateDistance](@pickup_latitude, @pickup_longitude, @dropoff_latitude, @dropoff_longitude) AS direct_distance
  
      )
    GO
    ```

    + This table-valued function that takes multiple columns as inputs, and outputs a table with multiple feature columns.

    + The purpose of this function is to create new features for use in building a model.

2.  To verify that this function works, use it to calculate the geographical distance for those trips where the metered distance was 0 but the pick-up and drop-off locations were different.
  
    ```sql
        SELECT tipped, fare_amount, passenger_count,(trip_time_in_secs/60) as TripMinutes,
        trip_distance, pickup_datetime, dropoff_datetime,
        dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) AS direct_distance
        FROM nyctaxi_sample
        WHERE pickup_longitude != dropoff_longitude and pickup_latitude != dropoff_latitude and trip_distance = 0
        ORDER BY trip_time_in_secs DESC
    ```
  
    As you can see, the distance reported by the meter doesn't always correspond to geographical distance. This is why feature engineering is so important. You can use these improved data features to train a machine learning model using R.

## Next lesson

[Lesson 3: Train and save a model using T-SQL](sqldev-train-and-save-a-model-using-t-sql.md)

## Previous lesson

[Lesson 1: Explore and visualize the data using R and stored procedures](sqldev-explore-and-visualize-the-data.md)
