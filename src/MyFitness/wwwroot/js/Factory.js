let APIkey = GetKey();

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
            resolve(err)
        })
    })
}