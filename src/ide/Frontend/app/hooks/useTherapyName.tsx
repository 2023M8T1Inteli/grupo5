import { useState } from 'react';

function useTherapyName(initialName: string): [string, boolean, () => void, (event: React.ChangeEvent<HTMLInputElement>) => void, () => void] {
	const [therapyName, setTherapyName] = useState(initialName);
	const [isEditing, setIsEditing] = useState(false);

	const handleNameClick = () => {
		setIsEditing(true);
	};
	
	const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setTherapyName(event.target.value);
	};
	
	const handleNameBlur = () => {
		setIsEditing(false);
	};

	return [therapyName, isEditing, handleNameClick, handleNameChange, handleNameBlur];
}

export default useTherapyName;