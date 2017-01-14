function SearchForFoods(FoodName) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: `http://api.nal.usda.gov/ndb/search/?format=json&q=${FoodName}&sort=n&max=25&offset=0&api_key=lW6xOCdaknhH5uu4AEzm17xeoeKD5szhgveUQ7ja`
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
            url: `http://api.nal.usda.gov/ndb/nutrients/?format=json&api_key=lW6xOCdaknhH5uu4AEzm17xeoeKD5szhgveUQ7ja&nutrients=205&nutrients=204&nutrients=208&nutrients=269&ndbno=${FoodId}`
        }).done(function (item) {
            resolve(item.report.foods)
        }).error(function (err) {
            resolve(err)
        })
    })
}

SearchForFoods("bread")
.then(function (foods) {
    FindFoodNutritionalValue("45044694")
    .then(function (FoodNutritionValue) {
    })
})

$(".SecondaryLogin").hide()
$(".ShowNextRegister").on("click", function () {
    $(".InitialRegister").fadeOut(1000, function () {
        $(".SecondaryLogin").fadeIn(1000)
    })
})

$(".BackToMainLogin").on("click", function () {
    $(".SecondaryLogin").fadeOut(1000, function () {
        $(".InitialRegister").fadeIn(1000)
    })
})

$(".ChangePImageInput").on("change", function () {
    $(".ChangeProfileImg").submit()
})