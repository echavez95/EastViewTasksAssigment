<template>
    <input :disabled="disabled" :class="inputClass" 
            :id="id" 
            :type="type" 
            ref="input" 
            :value="modelValue" @input="handleInput($event)"/>
    <label class="active" :for="id">{{label}}</label>
    <span class="helper-text">{{errorMessage}}</span>
</template>
<script>
export default {
    name: "Matinput",
    inheritAttrs: false,
    data: () => ({
        errorMessage: '',
        inputClass: 'validate'
    }),
    props: {
        id: String,
        type: String,
        label: String,
        modelValue: String,
        disabled: String
    },
    emits: ['update:modelValue'],
    methods: {
        handleInput(e){
            this.$emit('update:modelValue', e.target.value);
            this.inputClass = 'valid';
            this.errorMessage = '';
        },
        focusInput() {
            this.$refs.input.focus();
        },
        setErrorMessage(message) {
            this.inputClass = 'invalid';
            this.errorMessage = message;
        },
        resetInput(){
            this.inputClass = 'validate';
            this.errorMessage = '';
        }
    }
}
</script>