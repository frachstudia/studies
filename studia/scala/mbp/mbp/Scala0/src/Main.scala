package roman

/**
 * Created by frach on 25.11.14.
 */
class Change(number: Int) {
    var i = 0
    var result = ""
    var in = number

    while (in != 0) {
        while (in > 1000) {
            result += "M"
            in = in - 1000
        }

        if (in >= 900) {
            result += "CM"
            in = in - 900
        }

        if (in >= 500) {
            result += "D"
            in = in - 500
        }

        if (in >= 400) {
            result += "Rosja"
        }
    }
}

object Main extends Application {

}
