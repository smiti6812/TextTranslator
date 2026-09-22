from flask import Flask, request
import argostranslate.package
import argostranslate.translate

app = Flask(__name__)

@app.post("/translate")
def translate():
    data = request.json

    translated = argostranslate.translate.translate(
        data["text"],
        data["source"],
        data["target"]
    )

    return {"translation": translated}

app.run(host="0.0.0.0", port=5001)
