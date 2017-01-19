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

$(".CloseEditModal").on("click", function () {
    $(".EditItem").fadeOut(1000)
})

$(".AddFood").on("click", function () {
    $(".AddExerciseModal").hide()
    $(".AddFoodModal").fadeIn(1000)
    $(".EditItem").hide()
})

$(".AddWorkout").on("click", function () {
    $(".AddFoodModal").hide()
    $(".AddExerciseModal").fadeIn(1000)
    $(".EditItem").hide()
})


$(".SecondaryLogin, .AddFoodModal, .AddExerciseModal, .EditItem").hide()
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

GetNutritionInformation()
.then(function (NutritionArray) {
    CreateBarChart(NutritionArray)
})

GetCaloriesConsumedInDateRange(7)
.then(function (CalorieInfo) {
    CreateLineChart(CalorieInfo)
})

GetNutritionGramsConsumedInformation()
.then(function (NInfo) {
    MakeCaloricPieChart(NInfo)
})

GetWeeklyInformationForLineChart()
.then(function (WLInfo) {
    WeeklyWeightLostLineChart(WLInfo)
})

GetPercentOfCaloriesInRange(7)
.then(function (CInfo) {
    CreatePolarChart(CInfo)
})

$(".Editable").on("click", function () {
    let LabelText = $(this).children(".NavLabel").text()
    let Value = $(this).children(".NavValue").text()

    $(".EditItemModal").html("")

    if(!$(this).hasClass("Height")){
        $(".EditItemModal").prepend(`
            <div class ="form-group">
                <label for="EditSomething" class ="col-md-4 control-label">${LabelText} </label>
                <div class ="col-md-6">
                    <input type="number" step="0.5" min="0" value="${Value}" class ="EditedValue form-control"></input>
                </div>
            </div>
        `)
        LabelText === "Age: " ? $(".SubmitEdit").attr("id", "EditAge") : false;
        LabelText === "Goal Weight: " ? $(".SubmitEdit").attr("id", "GoalWeight") : false;
        LabelText === "Current Weight: " ? $(".SubmitEdit").attr("id", "CurrentWeight") : false;
    }
    else {
        let HeightVal = Value.split(" ")

        $(".EditItemModal").prepend(`
            <div class="form-group">
            <label class="col-md-1 control-label">Feet: </label>
            <div class="col-md-4">
                <select class ="form-control EditFeet EditedValue">
                    <option>4</option>
                    <option>5</option>
                    <option>6</option>
                    <option>7</option>
                </select>
            </div>

            <label class="col-md-2 control-label">Inches: </label>
            <div class="col-md-4">
                <select class="form-control EditInches EditedValue" value="${HeightVal[2]}">
                    <option>0</option>
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>
                    <option>6</option>
                    <option>7</option>
                    <option>8</option>
                    <option>9</option>
                    <option>10</option>
                    <option>11</option>
                </select>
            </div>
        </div>`)

        $(".EditFeet").val(HeightVal[0])
        $(".EditInches").val(HeightVal[2])
    }
    $(".AddFoodModal").hide()
    $(".AddExerciseModal").hide()
    $(".EditItem").fadeIn(1000)
})

$(".SubmitEdit").on("click", function () {
    let Values = $(".EditedValue").toArray();
    if (Values.length > 1) {
        let Height = $(Values[0]).val()
        let Inches = $(Values[1]).val()
    
        EditUserHeight(Height, Inches)
        .then(function () {
            location.reload();
        })
    }
    else if ($(this).attr("id") === "EditAge") {
        let EditedValues = $(Values[0]).val()
        let AjaxObj = {
            "Age": EditedValues,
            "EditType": "Age"
        }
        EditUserValues(AjaxObj)
        .then(function () {
            location.reload();
        })
    }
    else if ($(this).attr("id") === "GoalWeight") {
        let EditedValues = $(Values[0]).val()
        let AjaxObj = {
            "GoalWeight": EditedValues,
            "EditType": "GoalWeight"
        }

        EditUserValues(AjaxObj)
        .then(function () {
            location.reload();
        })
    }
    else if ($(this).attr("id") === "CurrentWeight") {
        let EditedValues = $(Values[0]).val()
        let AjaxObj = {
            "CurrentWeight": EditedValues,
            "EditType": "CurrentWeight"
        }

        EditUserValues(AjaxObj)
        .then(function () {
            location.reload();
        })
    }

})