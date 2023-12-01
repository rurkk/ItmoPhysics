import React, { Component } from 'react';

export class FetchData extends Component {
    state = {
        file: null,
        cellCount: 0,
        xColumnNames: [],
        yColumnNames: [],
        validationResult: null,
        processDataResult: null,
        filesIds: [],  // Изменение на пустой массив
    };

    handleFileChange = async (e) => {
        const file = e.target.files[0];
        this.setState({ file });
        await this.validateFile(file);
    };

    handleCellCountChange = (e) => {
        this.setState({ cellCount: e.target.value });
    };

    handleXColumnNamesChange = (e, index) => {
        const newColumns = [...this.state.xColumnNames];
        newColumns[index] = e.target.value;
        this.setState({ xColumnNames: newColumns });
    };

    handleYColumnNamesChange = (e, index) => {
        const newColumns = [...this.state.yColumnNames];
        newColumns[index] = e.target.value;
        this.setState({ yColumnNames: newColumns });
    };

    validateFile = async (file) => {
        const formData = new FormData();
        formData.append('file', file);

        try {
            const response = await fetch('api/UploadMatrix/validate-file', {
                method: 'POST',
                body: formData,
                headers: {
                    'enctype': 'multipart/form-data',
                },
            });

            const result = await response.json();
            this.setState({ validationResult: result.message, filesIds: result.filesIds, cellCount: result.cellCount });
        } catch (error) {
            console.error('Error validating file: ', error.message);
            this.setState({ validationResult: 'Error validating file: ' + error.message });
        }
    };

    processData = async () => {
        const { cellCount, xColumnNames, yColumnNames, filesIds } = this.state;

        const formData = new FormData();
        formData.append('cellCount', cellCount);

        filesIds.forEach((fileId, index) => {
            formData.append(`filesIds[${index}]`, fileId);
        });

        xColumnNames.forEach((columnName, index) => {
            formData.append(`xColumnNames[${index}]`, columnName);
        });

        yColumnNames.forEach((columnName, index) => {
            formData.append(`yColumnNames[${index}]`, columnName);
        });

        try {
            const response = await fetch('api/UploadMatrix/process-data', {
                method: 'POST',
                body: formData,
                headers: {
                    'enctype': 'multipart/form-data',
                },
            });

            const result = await response.text();
            this.setState({ processDataResult: result });
        } catch (error) {
            console.error('Error processing data: ', error.message);
            this.setState({ processDataResult: 'Error processing data: ' + error.message });
        }
    };

    render() {
        return (
            <div>
                <input type="file" onChange={this.handleFileChange} />
                <p>Validation Result: {this.state.validationResult}</p>

                {this.state.validationResult === 'File is valid' && (
                    <div>
                        {Array.from({ length: this.state.cellCount }, (_, index) => (
                            <div key={index} style={{ marginBottom: '10px' }}>
                                <input
                                    type="text"
                                    value={this.state.xColumnNames[index] || ''}
                                    onChange={(e) => this.handleXColumnNamesChange(e, index)}
                                    placeholder={`X Column ${index + 1}`}
                                />
                                <input
                                    type="text"
                                    value={this.state.yColumnNames[index] || ''}
                                    onChange={(e) => this.handleYColumnNamesChange(e, index)}
                                    placeholder={`Y Column ${index + 1}`}
                                />
                            </div>
                        ))}
                        <br />
                        <button onClick={this.processData}>Process Data</button>
                        <p>Process Data Result: {this.state.processDataResult}</p>
                    </div>
                )}
            </div>
        );
    }
}