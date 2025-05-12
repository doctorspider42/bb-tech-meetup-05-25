import React from 'react';
import styled from 'styled-components';
import { useEmail } from '../contexts/EmailContext';
import { useTheme } from '../contexts/ThemeContext';
import presets from '../presets';

const TabContainer = styled.div`
  display: flex;
  margin-bottom: var(--spacing-md, 1rem);
  flex-wrap: wrap;
  gap: var(--spacing-sm, 0.5rem);
  
  @media (max-width: 768px) {
    flex-direction: column;
  }
`;

const Tab = styled.button.attrs({
  type: 'button' // Explicitly set button type to prevent form submission
})`
  padding: var(--spacing-sm, 0.5rem) var(--spacing-md, 1rem);
  background-color: ${props => props.active ? 'var(--color-primary)' : 'var(--color-surface)'};
  color: ${props => props.active ? 'white' : 'var(--color-text)'};
  border: 1px solid ${props => props.active ? 'var(--color-primary)' : 'var(--color-border)'};
  border-radius: var(--border-radius-medium, 8px);
  cursor: pointer;
  transition: all 0.2s;
  font-weight: ${props => props.active ? '700' : '400'};
  flex-grow: 1;
    &:hover {
    background-color: ${props => props.active ? 'var(--color-primary-dark)' : 'var(--color-background)'};
  }
  
  &:focus {
    outline: none;
    box-shadow: 0 0 0 2px var(--color-primary-light);
  }
`;

const PresetDescription = styled.div`
  font-size: var(--font-size-sm, 0.875rem);
  color: var(--color-text-light);
  margin-bottom: var(--spacing-md, 1rem);
  transition: color 0.3s ease;
`;

const PresetTabs = () => {
  const { emailContent, setEmailContent } = useEmail();
  
  // Find the current active preset or default to the first one
  const activePreset = presets.find(preset => preset.content === emailContent) || presets[0];
    const handleTabClick = (preset, event) => {
    // Prevent any default behavior
    if (event) {
      event.preventDefault();
      event.stopPropagation();
    }
    
    // Immediately fill in the template when the tab is clicked
    setEmailContent(preset.content);
  };
  
  return (
    <>
      <TabContainer>
        {presets.map((preset, index) => (          <Tab 
            key={index}
            active={preset.name === activePreset.name}
            onClick={(e) => handleTabClick(preset, e)}
          >
            {preset.name}
          </Tab>
        ))}
      </TabContainer>
      <PresetDescription>
        {activePreset.description}
      </PresetDescription>
    </>
  );
};

export default PresetTabs;
