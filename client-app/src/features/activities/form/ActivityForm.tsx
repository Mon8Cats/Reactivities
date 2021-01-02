import React, { FormEvent, useState } from 'react'
import { Button, Form, Segment } from 'semantic-ui-react'
import { IActivity } from '../../../app/models/activity'
import { v4 as uuid } from 'uuid';  

interface IProps {
    activity: IActivity | null  ;
    setEditMode: (editMode:boolean) => void;
    createActivity: (activity: IActivity) => void;
    editActivity: (activity: IActivity) => void;
    submitting: boolean;
}

export const ActivityForm : React.FC<IProps> = ({
    activity: initialFormState, 
    setEditMode,
    createActivity,
    editActivity,
    submitting
}) => {
    const initializeForm = () => {
        if (initialFormState) {
            return initialFormState
        } else {
            return {
                id: '',
                title: '',
                category: '',
                description: '',
                date: '',
                city: '',
                venue: ''
            }
        }
    };

    const [activity, setActivity] = useState<IActivity>(initializeForm);

    const handleInputChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const {name, value} = event.currentTarget
        setActivity({...activity, [name] : value})
    }

    const handleSubmit = () => {
        if (activity.id.length === 0){
            let newActivity = { ...activity, id: uuid()}
            createActivity(newActivity)
        } else {
            editActivity(activity);
        }
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit}>
                <Form.Input onChange={handleInputChange} name='title' placeholder='Title' value={activity.title}/>
                <Form.TextArea rows={2} onChange={handleInputChange} name='description' placeholder='Description' value={activity.description}/>
                <Form.Input onChange={handleInputChange} name='category' placeholder='Category' value={activity.category}/>
                <Form.Input type='datetime-local' onChange={handleInputChange} name='date' placeholder='Date' value={activity.date} />
                <Form.Input onChange={handleInputChange} placeholder='City' name='city' value={activity.city} />
                <Form.Input onChange={handleInputChange} placeholder='Venue' name='venue' value={activity.venue} />
                <Button loading={submitting} floated='right' positive type='submit' content='Submit' />
                <Button onClick={() => setEditMode(false)} floated='right' type='button' content='Cancel'/>
            </Form>
        </Segment>
    )
}



