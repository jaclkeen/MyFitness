let APIkey = GetKey();

function GetWeeklyInformationForLineChart() {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: "Home/WeeklyGoalWeightLossInformation"
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
            url: "/Home/CaloriesEatenAndRemaining"
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
            url: "/Home/NutritionInformation"
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
            url: `/Home/CaloriesConsumedInDateRange`,
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
            url: "Home/NutrientGrams"
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