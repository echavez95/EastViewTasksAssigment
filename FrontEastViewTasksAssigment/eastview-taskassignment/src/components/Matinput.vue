<template>
    <input :id="id" :type="type" class="validate" :value="modelValue" @input="$emit('update:modelValue', $event.target.value)">
    <label :for="id">{{label}}</label>
</template>
<script>
export default {
    name: "Matinput",
    inheritAttrs: false,
    props: {
        id: String,
        type: String,
        label: String,
        modelValue: String
    },
    emits: ['update:modelValue']
}
</script>