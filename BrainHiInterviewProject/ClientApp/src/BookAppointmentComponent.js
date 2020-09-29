import React, { Component } from 'react';

export default class BookAppointmentComponent extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <form action="/bookappointmentpost" method="post">
                <input type="hidden" name="ProviderId" value={`${this.props.providerId}`}></input>
                <input type="time" name="StartTime"></input>
                <input type="time" name="EndTime"></input>
                <input type="text" name="AppointmentReason"></input>
                <input type="text" name="PatientFullName"></input>
                <input type="text" name="PatientGender"></input>
                <input type="date" name="PatientDOB"></input>
                <input type="tel" name="PatientPhoneNumber"></input>
            </form>
        );
    }
}