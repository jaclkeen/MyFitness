function AppendFoodToDOM(food) {
    $(".FoodSearchResults").append(`
        <div class='FoodResult' id='${food.ndbno}'>
            <p class='FoodResultText'>${food.name}</p>
        </div>
    `)
}

$(".FoodSearch").on("input", function () {
    let InputValue = $(this).val()

    if (InputValue.length > 2) {
        SearchForFoods(InputValue)
        .then(function (foods) {
            $(".FoodSearchResults").html("")
            foods.forEach(function (food) {
                AppendFoodToDOM(food)
            })
        })
    }
    else {
        $(".FoodSearchResults").html("")
    }
})

$(".FoodSearchResults").on("click", function (e) {
    let context = $(e.target)
    if (context.hasClass("FoodResult")) {
        let FoodId = context.attr("id")
        FindFoodNutritionalValue(FoodId)
        .then(function (FoodNutritionValues) {
            $(".FoodSearchResults").html("")
            $(".FoodSearch").val(`${FoodNutritionValues[0].name}`)
            $(".Fat").val(`${FoodNutritionValues[0].nutrients[2].gm}`)
            $(".Calories").val(`${FoodNutritionValues[0].nutrients[0].gm}`)
            $(".Carbs").val(`${FoodNutritionValues[0].nutrients[3].gm}`)
        })
    }
    else if (context.hasClass("FoodResultText")) {
        let FoodId = context.parent().attr("id")
        FindFoodNutritionalValue(FoodId)
        .then(function (FoodNutritionValues) {
            $(".FoodSearchResults").html("")
            $(".FoodSearch").val(`${FoodNutritionValues[0].name}`)
            $(".Fat").val(`${FoodNutritionValues[0].nutrients[2].gm}`)
            $(".Calories").val(`${FoodNutritionValues[0].nutrients[0].gm}`)
            $(".Carbs").val(`${FoodNutritionValues[0].nutrients[3].gm}`)
        })
    }
})

$(".CloseFoodModal").on("click", function () {
    $(".AddFoodModal").fadeOut(1000)
})

$(".CloseExerciseModal").on("click", function () {
    $(".AddExerciseModal").fadeOut(1000)
})

$(".AddFood").on("click", function () {
    $(".AddExerciseModal").hide()
    $(".AddFoodModal").fadeIn(1000)
})

$(".AddWorkout").on("click", function () {
    $(".AddFoodModal").hide()
    $(".AddExerciseModal").fadeIn(1000)
})

$(".SecondaryLogin, .AddFoodModal, .AddExerciseModal").hide()
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

CaloriesEatenAndRemaining()
.then(function (CalRemaining) {
    MakePieChart(CalRemaining)
})