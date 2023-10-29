
## `SessionViewSet`

### List All Sessions

- **URL:** `/sessions/`
- **Method:** `GET`
  
**Sample Response:**
```json
[
    {
        "SessionID": 1,
        "testssenderReply": {
            "TestID": 101,
            "TestName": "Math Test"
        },
        "SessionDate": "2023-10-29T14:15:22Z",
        "DurationSession": "00:30:00",
        "IsSuccessful": true,
        "FailureReason": "",
        "Score": 85,
        "MaxScore": 100
    },
    ...
]
```

### Retrieve Specific Session

- **URL:** `/sessions/1/`
- **Method:** `GET`

**Sample Response:**
```json
{
    "SessionID": 1,
    "testssenderReply": {
        "TestID": 101,
        "TestName": "Math Test"
    },
    "SessionDate": "2023-10-29T14:15:22Z",
    "DurationSession": "00:30:00",
    "IsSuccessful": true,
    "FailureReason": "",
    "Score": 85,
    "MaxScore": 100
}
```

---

## `TestsViewSet`

### List All Tests

- **URL:** `/tests/`
- **Method:** `GET`

**Sample Response:**
```json
[
    {
        "TestID": 101,
        "TestName": "Math Test"
    },
    ...
]
```

### Retrieve Specific Test

- **URL:** `/tests/101/`
- **Method:** `GET`

**Sample Response:**
```json
{
    "TestID": 101,
    "TestName": "Math Test"
}
```

---

## `QuestionViewSet`

### List All Questions for a Test

- **URL:** `/questions/?test_id=101`
- **Method:** `GET`

**Sample Response:**
```json
[
    {
        "QuestionID": 1,
        "QuestionText": "What is 2+2?",
        "MaxScore": 10,
        "TypeQuestion": {
            "TypeID": 1,
            "TypeName": "Multiple Choice"
        },
        "Picture": {
            "PictureID": 201,
            "PathPicture": "/path/to/image.jpg"
        }
    },
    ...
]
```

### Retrieve Answer Variants for a Question

- **URL:** `/questions/1/`
- **Method:** `GET`

**Sample Response:**
```json
[
    {
        "CodeofAnswer": 1,
        "IsCorrect": true,
        "QuestionID": 1,
        "TextVariant": "4"
    },
    ...
]
```

### List Open Question Answers

- **URL:** `/questions/list_open_question_answers/?user_id=1&test_id=101`
- **Method:** `GET`

**Sample Response:**
```json
[
    {
        "AnswerID": 1,
        "UserAnswerText": "4",
        "CorrectAnswer": "4",
        "UserID": 1,
        "Score": 10,
        "QuestionID": 1
    },
    ...
]
```

### List Answers of a User for a Test

- **URL:** `/questions/list_answers_user/?user_id=1&test_id=101`
- **Method:** `GET`

**Sample Response:**
```json
[
    {
        "CodeofAnswer": 1,
        "IsCorrect": true,
        "QuestionID": 1,
        "TextVariant": "4"
    },
    ...
]
```

---

Эти примеры предоставляют основную идею о том, как будет выглядеть запрос и ответ для каждой конечной точки.