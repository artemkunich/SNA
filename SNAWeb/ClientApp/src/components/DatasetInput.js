import React, { useState } from 'react';
import Spinner from 'react-bootstrap/Spinner'
import axios from 'axios';

const initialFieldValue = {
    name: '',
    description: '',
    data: '',
    fileKey: 0
}

export default function DatasetInput(props) {

    const [values, setValues] = useState(initialFieldValue);
    const [spinner, setSpinner] = useState(false);

    /*const handleFileChange = e => {
        if (e.target.files && e.target.files[0]) {
            let datasetFile = e.target.files[0];
            const reader = new FileReader();

            reader.onload = x => {
                setValues({
                    ...values,
                    data: x.target.result,
                })
            }
            reader.readAsText(datasetFile)
        }
    }*/

    const handleFileChange = e => {
        if (e.target.files && e.target.files[0]) {
            const datasetFile = e.target.files[0];
            setValues({
                ...values,
                data: datasetFile,
            })
        }
    }

    const handleInputChange = e => {
        const { name, value } = e.target;

        setValues({            
            ...values,
            [name]: value
        })
    }

    const validate = () => {
        if (values.name == "") {
            props.openMessage("Name is required");
            return false;
        }
        if (values.data == "") {
            props.openMessage("There is no data");
            return false;
        }
        return true;
    }

    /*const handleUpload =  (e) => {
        if (!validate()) { return; }
        setSpinner(true);
        const request = {
            name: values.name,
            description: values.description,
            data: values.data
        };
        addDataset(request, () => {
            setValues({
                ...values,
                name: '',
                description: '',
                data: '',
                fileKey: values.fileKey + 1
            });
            setSpinner(false);
            props.onUpload();
        });
    }*/

    const handleUpload = (e) => {
        if (!validate()) { return; }
        setSpinner(true);
        const formData = new FormData();
        formData.append('name', values.name);
        formData.append('description', values.description);
        formData.append('file', values.data);

        addDataset(formData, () => {
            setValues({
                ...values,
                name: '',
                description: '',
                data: null,
                fileKey: values.fileKey + 1
            });
            setSpinner(false);
            props.onUpload();
        });
    }

    const addDataset = (request, onSuccess) => {
        datasetAPI().create(request)
            .then(res => {
                onSuccess();
            })
            .catch(error => {
                setSpinner(false);
                console.log(error);
                if (error.response && error.response.data && error.response.data.errorMessage) {
                    props.openMessage(error.response.data.errorMessage);
                }
            }
        )
    }

    const datasetAPI = (url = '/Datasets/File') => {
        return {
            create: newRecord => axios.post(url, newRecord),
            update: (id, updateRecord) => axios.put(url + id, updateRecord),
            delete: id => axios.delete(url + id)
        }
    }

    return (
        <>
            <div className="container text-center">
                <h1>New Dataset</h1>
            </div>
            <form autoComplete="off" noValidate>
                <div className="card">
                    <div className="card-body row">
                        <div className="col-md-8">
                            <div className="form-group">
                                <input className="form-control" placeholder="Name" name="name"
                                    value={values.name}
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="form-group">
                                <input className="form-control" placeholder="Description" name="description"
                                    value={values.description}
                                    onChange={handleInputChange}
                                />
                            </div>
                        </div>
                        <div className="col-md-4">
                            <div className="form-group">
                                <input type="file" accept="*.txt" className="form-control-file"
                                    key={values.fileKey}
                                    onChange={handleFileChange}
                                />
                            </div>
                            <div className="form-group">
                                <button type="button" className="btn btn-light" onClick={handleUpload}>                                 
                                    {spinner ? < Spinner animation="border" size="sm" variant="primary"/> : null} Upload                                
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>

        </>    
    )
}