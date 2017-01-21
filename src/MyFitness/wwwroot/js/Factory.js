let APIkey = GetKey();

function MacronutrientBreakdown() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/YearInformation/MacronutrientBreakdown"
        }).done(function (NBreakdown) {
            resolve(NBreakdown)
        }).error(function (err) {
            reject(err)
        })
    })
}

function YearlyMonthTotalWeightLostBreakdown() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/YearInformation/WeightLostBreakdown"
        }).done(function (WeightLossInfo) {
            resolve(WeightLossInfo)
        }).error(function (err) {
            reject(err)
        })
    })
}

function GetCaloriesStartedAndGoalInDayRange(TimeRange) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `/DailyAndWeekly/StartingDailyCalorieInformation`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(TimeRange)
        }).done(function (CalsBurned) {
            resolve(CalsBurned)
        }).error(function (err) {
            reject(err)
        })
    })
}

function GetCaloriesBurnedInRange(TimeRange) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `/DailyAndWeekly/CaloriesBurnedInTimeRange`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(TimeRange)
        }).done(function (CalsBurned) {
            resolve(CalsBurned)
        }).error(function (err) {
            reject(err)
        })
    })
}

function GetInformationForLineChartInRange(DayRange) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "DailyAndWeekly/WeightLossInformationInRange",
            method: "POST",
            contentType: 'application/json',
            data: JSON.stringify(DayRange)
        }).done(function (WLInfo) {
            resolve(WLInfo)
        }).error(function (err) {
            reject(err)
        })
    })
}

function SearchForFoods(FoodName) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `http://api.nal.usda.gov/ndb/search/?format=json&q=${FoodName}&sort=n&max=10&offset=0&api_key=${APIkey}`
        }).done(function (foods) {
            resolve(foods.list.item)
        }).error(function (err) {
            resolve(err)
        })
    })
}

function FindFoodNutritionalValue(FoodId) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `http://api.nal.usda.gov/ndb/nutrients/?format=json&api_key=${APIkey}&nutrients=205&nutrients=204&nutrients=208&nutrients=269&ndbno=${FoodId}`
        }).done(function (item) {
            resolve(item.report.foods)
        }).error(function (err) {
            resolve(err)
        })
    })
}

function CaloriesEatenAndRemaining() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/DailyAndWeekly/CaloriesEatenAndRemaining"
        }).done(function (CalInfo) {
            resolve(CalInfo)
        }).error(function (err) {
            reject(err)
        })
    })
}

function GetNutritionInformation() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "/DailyAndWeekly/NutritionInformation"
        }).done(function (NInfo) {
            resolve(NInfo)
        }).error(function (err) {
            reject(err)
        })
    })
}

function GetCaloriesConsumedInDateRange(DateRange) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `/DailyAndWeekly/CaloriesConsumedInDateRange`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(DateRange)
        }).done(function (CaloriesList) {
            resolve(CaloriesList)
        }).error(function (err) {
            reject(err)
        })
    })
}

function GetNutritionGramsConsumedInformation() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "DailyAndWeekly/NutrientGrams"
        }).done(function (NGrams) {
            resolve(NGrams)
        }).error(function (err) {
            reject(err)
        })
    })
}

function EditUserHeight(f, i) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "Home/UserHeight",
            method: "PATCH",
            contentType: "application/json",
            data: JSON.stringify({ feet: f, inches: i })
        }).done(function (n) {
            resolve(n)
        }).error(function (err) {
            reject(err)
        })
    })
}

function EditUserValues(DataObj) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "Home/UserInformation",
            method: "PATCH",
            contentType: "application/json",
            data: JSON.stringify(DataObj)
        }).done(function (n) {
            resolve(n)
        }).error(function (err) {
            reject(err)
        })
    })
}

function GetPercentOfCaloriesInRange(DateRange) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `/DailyAndWeekly/CaloricPercentageInformationInDateRage`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(DateRange)
        }).done(function (CaloriesList) {
            resolve(CaloriesList)
        }).error(function (err) {
            reject(err)
        })
    })
}